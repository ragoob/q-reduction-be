using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QReduction.Apis.Controllers;
using QReduction.Apis.Infrastructure;
using QReduction.Apis.Models;
using QReduction.Core.Domain.Acl;
using QReduction.Core.Models;
using QReduction.Core.Service.Generic;

namespace QReduction.QReduction.Infrastructure.DbMappings.Domain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableCors("QReductionPolicy")]
    [ValidateModelFilter]
    [ApiExplorerSettings(GroupName = "Admin")]

    public class HelpSupportController : CustomBaseController
    {
        #region Fields
        private readonly IService<HelpAndSupport> _HelpAndSupportService;



        #endregion

        #region ctor
        public HelpSupportController(IService<HelpAndSupport> HelpAndSupportService)
        {
            _HelpAndSupportService = HelpAndSupportService;

        }
        #endregion

        #region Actions




        [HttpPost]
        [CustomAuthorizationFilter("About.Add")]
        [ApiExplorerSettings(GroupName = "Mobile")]
        public async Task<IActionResult> Post(HelpSupport helpSupport)
        {

            HelpAndSupport helpAndSupport = new HelpAndSupport()
            {
                MessageTitle = helpSupport.MessageTitle,
                Message = helpSupport.Message,
                

            };
            helpAndSupport.UserId = UserId;

            if (helpSupport.UserId != null)
                helpAndSupport.UserId = helpSupport.UserId;

           
            helpAndSupport.CreateAt = DateTime.UtcNow;
            helpAndSupport.CreateBy = UserId;

            await _HelpAndSupportService.AddAsync(helpAndSupport);
            return Ok();
        }


        [HttpPost]
        [Route("organizationPostHelp")]
        [CustomAuthorizationFilter("About.Add")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> organizationPostHelp(HelpAndSupport helpAndSupport)
        {


            helpAndSupport.CreateAt = DateTime.UtcNow;
            helpAndSupport.CreateBy = UserId;

            await _HelpAndSupportService.AddAsync(helpAndSupport);
            return Ok();
        }

        [HttpPut]
        [CustomAuthorizationFilter("About.Edit")]
        [ApiExplorerSettings(GroupName = "Admin")]

        public async Task<IActionResult> Put(HelpAndSupport about)
        {



            about.UpdateAt = DateTime.UtcNow;
            about.UpdateBy = UserId;
            await _HelpAndSupportService.EditAsync(about);
            return Ok();
        }

        [HttpPut]
        [Route("RemoveToRecycleBin/{id}")]
        [CustomAuthorizationFilter("About.Delete")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> RemoveToRecycleBin(int id)
        {
            HelpAndSupport branch = await _HelpAndSupportService.GetByIdAsync(id);

            if (branch != null)
            {
                branch.DeletedAt = DateTime.UtcNow;
                branch.DeletedBy = UserId;
                branch.IsDeleted = true;
                await _HelpAndSupportService.EditAsync(branch);
            }

            return Ok();
        }



        [HttpPut]
        [Route("RestoreDeleted/{id}")]
        [CustomAuthorizationFilter("About.Restore")]
        [ApiExplorerSettings(GroupName = "Mobile")]
        public async Task<IActionResult> RestoreDeleted(int id)
        {
            var entity = await _HelpAndSupportService.GetByIdAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = false;
                entity.DeletedBy = null;
                entity.DeletedAt = null;
                await _HelpAndSupportService.EditAsync(entity);
            }
            return Ok();
        }

        [HttpGet]
        [Route("{currentPage}/{pageSize}")]
        [CustomAuthorizationFilter("About")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> Get(int currentPage, int pageSize,
            [FromQuery] string sortBy,
            [FromQuery] SearchOrders? sortOrder,
            [FromQuery] string searchText,
            [FromQuery] string nameAr,
            [FromQuery] string nameEn,

            [FromQuery] bool isDeleted)
        {
            PagedListModel<HelpAndSupport> pagedList = new PagedListModel<HelpAndSupport>(currentPage, pageSize);
            pagedList.QueryOptions.SortField = sortBy ?? pagedList.QueryOptions.SortField;
            pagedList.QueryOptions.SearchOrder = sortOrder ?? pagedList.QueryOptions.SearchOrder;

            pagedList.DataList = await
                _HelpAndSupportService.FindAsync(pagedList.QueryOptions,
                c => c.IsDeleted == isDeleted &&

                (nameAr == null || c.Message.Contains(nameAr)) &&
                (nameEn == null || c.MessageTitle.Contains(nameEn)) &&

                (string.IsNullOrWhiteSpace(searchText) ||
                    (c.Message.ToString().Contains(searchText) ||
                     c.MessageTitle.Contains(searchText))

                ),"User", "User.Organization", "User.Branch");

            return Ok(pagedList);
        }

        [HttpGet("{id}")]
        [CustomAuthorizationFilter("About")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> Get(int id)
        {
            HelpAndSupport about = (await _HelpAndSupportService.FindAsync(c => c.Id == id, "User.Organization", "User.Branch")).SingleOrDefault();
            return Ok(about);
        }

        [HttpGet]
        [Route("GetItemList/{lang}")]
        [ApiExplorerSettings(GroupName = "Admin")]
        [AllowAnonymous]
        public async Task<ActionResult> GetItemList(string lang)
        {
            bool isArabic = lang.Equals("ar", StringComparison.OrdinalIgnoreCase);
            IEnumerable<SelectItemModel> items = (await _HelpAndSupportService.GetAllAsync())
                .Select(info =>
                    new SelectItemModel
                    {
                        Value = info.Id,
                        Label = isArabic ? info.Message : info.Message
                    });

            return Ok(items);
        }



        #endregion



    }
}
