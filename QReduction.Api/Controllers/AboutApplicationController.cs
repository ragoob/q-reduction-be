using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QReduction.Api;
using QReduction.Apis.Controllers;
using QReduction.Apis.Infrastructure;
using QReduction.Apis.Models;
using QReduction.Core.Domain;
using QReduction.Core.Domain.Acl;
using QReduction.Core.Models;
using QReduction.Core.Service.Generic;

namespace QReduction.QReduction.Infrastructure.DbMappings.Domain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("QReductionPolicy")]
    [ValidateModelFilter]
    [ApiExplorerSettings(GroupName = "Admin")]

    public class AboutApplicationController : CustomBaseController
    {
        #region Fields
        private readonly IService<About> _AboutService;
  


        #endregion

        #region ctor
        public AboutApplicationController(IService<About> AboutService)
        {
            _AboutService = AboutService;
           
        }
        #endregion

        #region Actions


   

        [HttpPost]
       // [CustomAuthorizationFilter("About.Add")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> Post(List<About>  about)
        {

           
            foreach (var item in about)
            {
                item.CreateAt = DateTime.UtcNow;
                item.CreateBy = UserId;
                item.UpdateAt = null;
                item.DeletedAt = null;
                item.IsDeleted = false;
                
                await _AboutService.AddAsync(item);
            }
           
            return Ok();
        }

        [HttpPut]
        [CustomAuthorizationFilter("About.Edit")]
        [ApiExplorerSettings(GroupName = "Admin")]

        public async Task<IActionResult> Put(About about)
        {


           
            about.UpdateAt = DateTime.UtcNow;
            about.UpdateBy = UserId;
            await _AboutService.EditAsync(about);
            return Ok();
        }

        [HttpPut]
        [Route("RemoveToRecycleBin/{id}")]
        [CustomAuthorizationFilter("About.Delete")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> RemoveToRecycleBin(int id)
        {
            About branch = await _AboutService.GetByIdAsync(id);

            if (branch != null)
            {
                branch.DeletedAt = DateTime.UtcNow;
                branch.DeletedBy = UserId;
                branch.IsDeleted = true;
                await _AboutService.EditAsync(branch);
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
            PagedListModel<About> pagedList = new PagedListModel<About>(currentPage, pageSize);
            pagedList.QueryOptions.SortField = sortBy ?? pagedList.QueryOptions.SortField;
            pagedList.QueryOptions.SearchOrder = sortOrder ?? pagedList.QueryOptions.SearchOrder;

            pagedList.DataList = await
                _AboutService.FindAsync(pagedList.QueryOptions,
                c => c.IsDeleted == isDeleted &&
               
                (nameAr == null || c.LabelTextAr.Contains(nameAr)) &&
                (nameEn == null || c.LabelTextEn.Contains(nameEn)) &&
                
                (string.IsNullOrWhiteSpace(searchText) ||
                    (c.LabelTextAr.ToString().Contains(searchText) ||
                     c.LabelTextEn.Contains(searchText)) 
                    
                ));

            return Ok(pagedList);
        }

        [HttpGet("{id}")]
        [CustomAuthorizationFilter("About")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> Get(int id)
        {
            About  about = (await _AboutService.FindAsync(c => c.Id == id)).SingleOrDefault();
            return Ok(about);
        }

        [HttpGet("GetAboutApplication")]
        //[CustomAuthorizationFilter("About")]
        [ApiExplorerSettings(GroupName = "Mobile")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAboutApplication()
        {
            List<About> about = (await _AboutService.GetAllAsync()).ToList();
            return Ok(about);
        }
        [HttpPut]
        [Route("RestoreDeleted/{id}")]
        [CustomAuthorizationFilter("About.Restore")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> RestoreDeleted(int id)
        {
            var entity = await _AboutService.GetByIdAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = false;
                entity.DeletedBy = null;
                entity.DeletedAt = null;
                await _AboutService.EditAsync(entity);
            }
            return Ok();
        }
        [HttpGet]
        [Route("GetItemList/{lang}")]
        [ApiExplorerSettings(GroupName = "Admin")]
        [AllowAnonymous]
        public async Task<ActionResult> GetItemList(string lang)
        {
            bool isArabic = lang.Equals("ar", StringComparison.OrdinalIgnoreCase);
            IEnumerable<SelectItemModel> items = (await _AboutService.GetAllAsync())
                .Select(info =>
                    new SelectItemModel
                    {
                        Value = info.Id,
                        Label = isArabic ? info.LabelTextAr : info.LabelTextEn
                    });

            return Ok(items);
        }

  

        #endregion


       
    }
}
