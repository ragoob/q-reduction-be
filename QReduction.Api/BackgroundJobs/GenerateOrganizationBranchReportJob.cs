using IronPdf;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using QReduction.Core.Domain;
using QReduction.Infrastructure.DbContexts;
using QReduction.Apis.Infrastructure;
using Quartz;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.NodeServices;
using System.Drawing;

namespace QReduction.Api.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class GenerateOrganizationBranchReportJob : IJob
    {
        private IDatabaseContext _databaseContext;
        private IEmailSender _emailSender;

        private readonly IServiceProvider _serviceProvider;
        private readonly INodeServices _nodeServices;

        #region CommentedCtor
        //public GenerateOrganizationBranchReportJob(
        //    DatabaseContext databaseContext,
        //    IEmailSender emailSender,
        //    )
        //{
        //    _databaseContext = databaseContext;
        //    _emailSender = emailSender;
        //} 
        #endregion

        public GenerateOrganizationBranchReportJob(IServiceProvider serviceProvider, INodeServices nodeServices)
        {
            _serviceProvider = serviceProvider;
            _nodeServices = nodeServices;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    _databaseContext = scope.ServiceProvider.GetService<IDatabaseContext>();
                    _emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();


                    var JobRequests = _databaseContext.Set<JobRequest>()
                                            .Where(c => !c.IsDone && c.JobId == (int)Core.Job.BranchReport).
                                            Include(c => c.JobRequestParameters).ToList();


                    foreach (var _request in JobRequests)
                    {
                        var organization = _request.JobRequestParameters.FirstOrDefault(c => c.Name == "organiztionId");
                        var email = _request.JobRequestParameters.FirstOrDefault(c => c.Name == "Email");
                        var orgId = int.Parse(organization.Value);
                        var mail = email.Value;
                        await SendPdfFile(orgId, mail);
                        _request.IsDone = true;
                        _databaseContext.Set<JobRequest>().Update(_request);
                       await _databaseContext.SaveChangesAsync();

                    }
                    
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Execute function error {ex.Message}");

            }

        }

        private async Task SendPdfFile(int organiztionId, string mail)
        {
            try
            {
                var data = _databaseContext.Set<Branch>().Where(c => c.OrganizationId == organiztionId).AsNoTracking();

                Console.WriteLine($"Branches count {data.Count()}");
                var html = GetHtmlForOrganizationBranches(data);
                Console.WriteLine("Html generated successfully");
                //var file = await HtmlToPdf.StaticRenderHtmlAsPdfAsync(html);
                var file = await _nodeServices.InvokeAsync<byte[]>("./pdf",html);

                await _emailSender.SendMail(to: new string[] { mail }, $"Branchs {DateTime.Now.Date}", string.Empty, "application/pdf", file, "Branches.pdf");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SendPdfFile function error {ex.Message}");
               
            }
        }


        private string GetHtmlForOrganizationBranches(IEnumerable<Branch> branches)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(@"
                        <html>
                            <head>
                                <style>
                                .main-container{
                                  display: flex;
                                  align-items: center;
                                  justify-content: center;
                                  height: 100vh;
                                  width: 100%;

                                }
                                .content-area{
                                  width: 300px;
                                  height: 300px;
                                  margin-left:210px;  
                                  text-align: center;
                                
                                  padding: 30px;
                                  border-radius: 10px;
                                }
                               
                                </style>
                            </head>
                            <body>
                                ");


            foreach (var branch in branches)
            {

                string qrCode = GenerateQrCode(branch.QrCode);
                if (!string.IsNullOrEmpty(qrCode))
                {
                    stringBuilder.AppendFormat($@"<div class='main-container'>

                                 <div class='content-area'>
                                    <p>{branch.NameEn}</p>
                                   <p>{branch.NameAr}</p>
                                   <br />
                                   <img src='data:image/jpeg;base64,{qrCode}' width='450' height='450' />
                                 </div>
                                </div>
                                <div style='page-break-after: always;'> </div>
                            ");
                }


            }

            stringBuilder.Append(@"
                                </table>
                            </body>
                        </html>");

            return stringBuilder.ToString();

        }

        private string GenerateQrCode(string data)
        {
            try
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(data,
                QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(550);
                byte[] imageByte = BitmapToBytes(qrCodeImage);
                string base64String = Convert.ToBase64String(imageByte);

                return base64String;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Failed to generate QRCode {ex.Message}");
                return string.Empty;
            }
        }

        private  Byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }

    }
}
