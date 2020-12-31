using IronPdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using QRCoder;
using QReduction.Api;
using QReduction.Apis.Controllers;
using QReduction.Apis.Infrastructure;
using QReduction.Apis.Models;
using QReduction.Core.Domain;
using QReduction.Core.Models;
using QReduction.Core.Service.Generic;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;


namespace QReduction.QReduction.Infrastructure.DbMappings.Domain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("QReductionPolicy")]
    [ValidateModelFilter]
    [ApiExplorerSettings(GroupName = "OrganizationAdmin")]
    public class BranchController : CustomBaseController
    {
        #region Fields
        private readonly IService<Branch> _branchService;
        private readonly IService<BranchService> _branchServicesService;
        private readonly IService<Service> _servicesService;
       
        #endregion

        #region ctor
        public BranchController(IService<Branch> branchService, IService<BranchService> branchServicesService, IService<Service> servicesService)
        {
            _branchService = branchService;
            _branchServicesService = branchServicesService;
            _servicesService = servicesService;
        }
        #endregion

        #region Actions


        [HttpGet]
        [Route("GetBranchesByOrganizationId/{OrgId}")]
        [CustomAuthorizationFilter("Organization.Add")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> GetBranchesByOrganizationId(int OrgId)
        {
            if (OrgId == 0)
                return BadRequest();

            var branches = await _branchService.FindAsync(b => b.OrganizationId == OrgId);
            return Ok(branches);
        }

        [HttpPost]
        [CustomAuthorizationFilter("Branch.Add")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> Post(Branch branch)
        {
            if (await _branchService.AnyAsync(info => info.Code == branch.Code))
                return BadRequest(Messages.Exist_Code);

            if (await _branchService.AnyAsync(info => info.NameAr == branch.NameAr))
                return BadRequest(Messages.Exist_NameAr);

            if (await _branchService.AnyAsync(info => info.NameEn == branch.NameEn))
                return BadRequest(Messages.Exist_NameEn);

            branch.CreateAt = DateTime.UtcNow;
            branch.CreateBy = UserId;
            branch.OrganizationId = OrganizationId;
            branch.QrCode = Guid.NewGuid().ToString();
            await _branchService.AddAsync(branch);
            return Ok();
        }

        [HttpPut]
        [CustomAuthorizationFilter("Branch.Edit")]
        [ApiExplorerSettings(GroupName = "Admin")]

        public async Task<IActionResult> Put(Branch branch)
        {

            if (await _branchService.AnyAsync(info => info.Code == branch.Code && info.Id != branch.Id))
                return BadRequest(Messages.Exist_Code);

            if (await _branchService.AnyAsync(info => info.NameAr == branch.NameAr && info.Id != branch.Id))
                return BadRequest(Messages.Exist_NameAr);

            if (await _branchService.AnyAsync(info => info.NameEn == branch.NameEn && info.Id != branch.Id))
                return BadRequest(Messages.Exist_NameEn);

            branch.UpdateAt = DateTime.UtcNow;
            branch.UpdateBy = UserId;
            await _branchService.EditAsync(branch);
            return Ok();
        }

        [HttpPut]
        [Route("RemoveToRecycleBin/{id}")]
        [CustomAuthorizationFilter("Branch.Delete")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> RemoveToRecycleBin(int id)
        {
            Branch branch = await _branchService.GetByIdAsync(id);

            if (branch != null)
            {
                branch.DeletedAt = DateTime.UtcNow;
                branch.DeletedBy = UserId;
                branch.IsDeleted = true;
                await _branchService.EditAsync(branch);
            }

            return Ok();
        }

        [HttpPut]
        [Route("RestoreDeleted/{id}")]
        [CustomAuthorizationFilter("Branch.Restore")]
        [ApiExplorerSettings(GroupName = "OrganizationAdmin")]
        public async Task<IActionResult> RestoreDeleted(int id)
        {
            var entity = await _branchService.GetByIdAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = false;
                entity.DeletedBy = null;
                entity.DeletedAt = null;
                await _branchService.EditAsync(entity);
            }
            return Ok();
        }

        [HttpPut]
        [Route("MultiRemoveToRecycleBin/{ids}")]
        [CustomAuthorizationFilter("Branch.Delete")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> MultiRemoveToRecycleBin(string ids)
        {
            int[] idsToDelete = ids.Split(',').Select(Int32.Parse).ToArray();

            List<Branch> branchs = _branchService.Find(item => idsToDelete.Contains(item.Id)).ToList();
            if (branchs == null)
                return BadRequest(Messages.CanNotDeleteMulti);
            branchs.ForEach(e =>
            {
                e.DeletedAt = DateTime.UtcNow;
                e.DeletedBy = UserId;
                e.IsDeleted = true;
            });

            await _branchService.EditRangeAsync(branchs);
            return Ok();
        }

        [HttpPut]
        [Route("MultiRestoreDeleted/{ids}")]
        [CustomAuthorizationFilter("Branch.Restore")]
        [ApiExplorerSettings(GroupName = "OrganizationAdmin")]
        public async Task<IActionResult> MultiRestoreDeleted(string ids)
        {
            int[] idsToRestor = ids.Split(',').Select(Int32.Parse).ToArray();

            List<Branch> branchs = _branchService.Find(item => idsToRestor.Contains(item.Id)).ToList();
            if (branchs == null)
                return BadRequest(Messages.CanNotRestorMulti);
            branchs.ForEach(e =>
            {
                e.IsDeleted = false;
                e.DeletedBy = null;
                e.DeletedAt = null;
            });
            await _branchService.EditRangeAsync(branchs);
            return Ok();
        }

        [HttpGet]
        [Route("{currentPage}/{pageSize}")]
        [CustomAuthorizationFilter("Branch")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> Get(int currentPage, int pageSize,
            [FromQuery] string sortBy,
            [FromQuery] SearchOrders? sortOrder,
            [FromQuery] string searchText,
            [FromQuery] int? code,
            [FromQuery] string nameAr,
            [FromQuery] string nameEn,
            [FromQuery] string phone,
            [FromQuery] bool isDeleted)
        {
            PagedListModel<Branch> pagedList = new PagedListModel<Branch>(currentPage, pageSize);
            pagedList.QueryOptions.SortField = sortBy ?? pagedList.QueryOptions.SortField;
            pagedList.QueryOptions.SearchOrder = sortOrder ?? pagedList.QueryOptions.SearchOrder;

            pagedList.DataList = await
                _branchService.FindAsync(pagedList.QueryOptions,
                c => c.IsDeleted == isDeleted && (c.OrganizationId == OrganizationId) &&
                (code == null || c.Code == code) &&
                (nameAr == null || c.NameAr.Contains(nameAr)) &&
                (nameEn == null || c.NameEn.Contains(nameEn)) &&
                (phone == null || c.Phone.Contains(phone)) &&
                (string.IsNullOrWhiteSpace(searchText) ||
                    (c.Code.ToString().Contains(searchText) ||
                     c.NameAr.Contains(searchText) ||
                     c.NameEn.Contains(searchText) ||
                     c.Phone.Contains(searchText))
                ));

            return Ok(pagedList);
        }

        [HttpGet("{id}")]
        [CustomAuthorizationFilter("Branch")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> Get(int id)
        {
            Branch branch = (await _branchService.FindAsync(c => c.Id == id,
                "BranchServices")).SingleOrDefault();
            return Ok(branch);
        }

        [HttpGet]
        [Route("GetItemList/{lang}")]
        [ApiExplorerSettings(GroupName = "Admin")]
        [AllowAnonymous]
        public async Task<ActionResult> GetItemList(string lang)
        {
            bool isArabic = lang.Equals("ar", StringComparison.OrdinalIgnoreCase);
            IEnumerable<SelectItemModel> items = (await _branchService.GetAllAsync())
                .Select(info =>
                    new SelectItemModel
                    {
                        Value = info.Id,
                        Label = isArabic ? info.NameAr : info.NameEn
                    });

            return Ok(items);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetNextCode")]
        [ApiExplorerSettings(GroupName = "OrganizationAdmin")]
        public async Task<IActionResult> GetNextCode()
        {
            int nextCode = (await _branchService.MaxAsync<int>(c => c.Code)) + 1;

            return Ok(new
            {
                nextCode
            });
        }

        #endregion


        #region orgFunctions

        [HttpGet]
        [Route("GetOrganizationBranchs")]
        [CustomAuthorizationFilter("Branch.GetBranchs")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> GetOrganizationBranchs()
        {
            List<Branch> branches = new List<Branch>();
            branches = (await _branchService.FindAsync(b => b.OrganizationId == OrganizationId)).ToList();
            return Ok(branches);
        }


        [HttpGet]
        [Route("GetOrganizationQRBranchesFile")]
        [CustomAuthorizationFilter("Branch.GetBranchs")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> GetOrganizationQRBranchesFile()
        {
            try
            {
                var data = (await _branchService.FindAsync(b => b.OrganizationId == OrganizationId)).AsQueryable();
                var html = GetHtmlForOrganizationBranches(data);
                var file = HtmlToPdf.StaticRenderHtmlAsPdf(html);

                return File(file.BinaryData, "application/pdf", "Branches.pdf");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new StatusCodeResult(500);
            }
        }
        // [HttpGet]
        // [Route("GetBranchServices")]
        //// [CustomAuthorizationFilter("Branch.GetBranchServices")]
        // [ApiExplorerSettings(GroupName = "Admin")]
        // public async Task<IActionResult> GetBranchServices(int branchId)
        // {
        //     // List<BranchService> branches = new List<BranchService>();
        //     var branches = (await _branchServicesService.FindAsync(b => b.BranchId == branchId, "Service")).SingleOrDefault();

        //     var q= (await _branchServicesService.FindAsync(a=>a.BranchId==branchId)).Join(_servicesService,"",)




        //     return Ok(branches);
        // }

        #endregion

        #region Helpers
   

        private string GetHtmlForOrganizationBranches(IQueryable<Branch> branches)
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

                stringBuilder.AppendFormat($@"<div class='main-container'>

                                 <div class='content-area'>
                                    <p>{branch.NameEn}</p>
                                   <p>{branch.NameAr}</p>
                                   <br />
                                   <img src='data:image/png;base64,{GenerateQrCode(branch.QrCode)}' width='350' height='350' />
                                 </div>
                                </div>
                                <div style='page-break-after: always;'> </div>
                            ");


            }

            stringBuilder.Append(@"
                                </table>
                            </body>
                        </html>");

            return stringBuilder.ToString();

        }

        private string GenerateQrCode(string data)
        {
            QRCodeGenerator Qr = new QRCodeGenerator();
            QRCodeData QrCodeData = Qr.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(QrCodeData);

            var QrImage = qrCode.GetGraphic(350);
            // Convert.ToBase64String(QrImage);
            var MemoryStream = new MemoryStream();
            QrImage.Save(MemoryStream, ImageFormat.Png);
            var bytes = MemoryStream.ToArray();

            var base64 = Convert.ToBase64String(bytes);

            MemoryStream.Dispose();
            return base64;
        }

        #endregion

    }
}