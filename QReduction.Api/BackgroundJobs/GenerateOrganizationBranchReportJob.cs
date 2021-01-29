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

namespace QReduction.Api.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class GenerateOrganizationBranchReportJob : IJob
    {
        private IDatabaseContext _databaseContext;
        private IEmailSender _emailSender;

        private readonly IServiceProvider _serviceProvider;


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

        public GenerateOrganizationBranchReportJob(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    _databaseContext = scope.ServiceProvider.GetService<IDatabaseContext>();
                    _emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();


                    var JobRequests = await _databaseContext.Set<JobRequest>()
                                            .Where(c => !c.IsDone && c.JobId == (int)Core.Job.BranchReport).
                                            Include(c => c.JobRequestParameters).ToListAsync();

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
                var data = await _databaseContext.Set<Branch>().Where(c => c.OrganizationId == organiztionId)
                    .ToListAsync();

                Console.WriteLine($"Branches count {data.Count()}");
                var html = GetHtmlForOrganizationBranches(data);
                Console.WriteLine("Html generated successfully");
                var file = await HtmlToPdf.StaticRenderHtmlAsPdfAsync(html);
                await _emailSender.SendMail(to: new string[] { mail }, $"Branchs {DateTime.Now.Date}", string.Empty, "application/pdf", file.BinaryData, "Branches.pdf");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SendPdfFile function error {ex.Message}");
                throw;
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
                                   <img src='data:image/png;base64,{qrCode}' width='450' height='450' />
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
            Console.WriteLine("Start generate qr code");
            QRCodeGenerator Qr = new QRCodeGenerator();
            QRCodeData QrCodeData = Qr.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(QrCodeData);

            var QrImage = qrCode.GetGraphic(550);
            // Convert.ToBase64String(QrImage);
            using (var MemoryStream = new MemoryStream())
            {
                try
                {

                    QrImage.Save(MemoryStream, ImageFormat.Png);
                    var bytes = MemoryStream.ToArray();
                    var base64 = Convert.ToBase64String(bytes);
                    return base64;
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"Error in GenerateQrCode function {ex.Message}");
                    MemoryStream.Dispose();
                    return string.Empty;
                }
            }
        }

    }
}