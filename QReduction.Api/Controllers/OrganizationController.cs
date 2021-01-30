using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using QReduction.Api;
using QReduction.Apis.Controllers;
using QReduction.Apis.Infrastructure;
using QReduction.Apis.Models;
using QReduction.Core.Domain;
using QReduction.Core.Models;
using QReduction.Core.Service.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QReduction.QReduction.Infrastructure.DbMappings.Domain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableCors("QReductionPolicy")]
    [ValidateModelFilter]
    public class OrganizationController : CustomBaseController
    {
        #region Fields
        private readonly IService<Organization> _OrganizationService;
        #endregion

        #region ctor
        public OrganizationController(IService<Organization> OrganizationService)
        {
            _OrganizationService = OrganizationService;
        }
        #endregion

        #region Actions

      



        [HttpPost]
        [CustomAuthorizationFilter("Organization.Add")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> Post(Organization organization)
        {
            if (await _OrganizationService.AnyAsync(info => info.Code == organization.Code))
                return BadRequest(Messages.Exist_Code);

            if (await _OrganizationService.AnyAsync(info => info.NameAr == organization.NameAr))
                return BadRequest(Messages.Exist_NameAr);

            if (await _OrganizationService.AnyAsync(info => info.NameEn == organization.NameEn))
                return BadRequest(Messages.Exist_NameEn);

            organization.CreateAt = DateTime.UtcNow;
            organization.CreateBy = UserId;
            await _OrganizationService.AddAsync(organization);
            return Ok();
        }

        [HttpPut]
        [CustomAuthorizationFilter("Organization.Edit")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> Put(Organization organization)
        {

            if (await _OrganizationService.AnyAsync(info => info.Code == organization.Code && info.Id != organization.Id))
                return BadRequest(Messages.Exist_Code);

            if (await _OrganizationService.AnyAsync(info => info.NameAr == organization.NameAr && info.Id != organization.Id))
                return BadRequest(Messages.Exist_NameAr);

            if (await _OrganizationService.AnyAsync(info => info.NameEn == organization.NameEn && info.Id != organization.Id))
                return BadRequest(Messages.Exist_NameEn);

            organization.UpdateAt = DateTime.UtcNow;
            organization.UpdateBy = UserId;
            await _OrganizationService.EditAsync(organization);
            return Ok();
        }

        [HttpPut]
        [Route("RemoveToRecycleBin/{id}")]
        [CustomAuthorizationFilter("Organization.Delete")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> RemoveToRecycleBin(int id)
        {
            Organization organization = await _OrganizationService.GetByIdAsync(id);

            if (organization != null)
            {
                organization.DeletedAt = DateTime.UtcNow;
                organization.DeletedBy = UserId;
                organization.IsDeleted = true;
                await _OrganizationService.EditAsync(organization);
            }

            return Ok();
        }

        [HttpPut]
        [Route("RestoreDeleted/{id}")]
        [CustomAuthorizationFilter("Organization.Restore")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> RestoreDeleted(int id)
        {
            var entity = await _OrganizationService.GetByIdAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = false;
                entity.DeletedBy = null;
                entity.DeletedAt = null;
                await _OrganizationService.EditAsync(entity);
            }
            return Ok();
        }

        [HttpPut]
        [Route("MultiRemoveToRecycleBin/{ids}")]
        [CustomAuthorizationFilter("Organization.Delete")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> MultiRemoveToRecycleBin(string ids)
        {
            int[] idsToDelete = ids.Split(',').Select(Int32.Parse).ToArray();

            List<Organization> Organizations = _OrganizationService.Find(item => idsToDelete.Contains(item.Id)).ToList();
            if (Organizations == null)
                return BadRequest(Messages.CanNotDeleteMulti);
            Organizations.ForEach(e =>
            {
                e.DeletedAt = DateTime.UtcNow;
                e.DeletedBy = UserId;
                e.IsDeleted = true;
            });

            await _OrganizationService.EditRangeAsync(Organizations);
            return Ok();
        }

        [HttpPut]
        [Route("MultiRestoreDeleted/{ids}")]
        [CustomAuthorizationFilter("Organization.Restore")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> MultiRestoreDeleted(string ids)
        {
            int[] idsToRestor = ids.Split(',').Select(Int32.Parse).ToArray();

            List<Organization> Organizations = _OrganizationService.Find(item => idsToRestor.Contains(item.Id)).ToList();
            if (Organizations == null)
                return BadRequest(Messages.CanNotRestorMulti);
            Organizations.ForEach(e =>
            {
                e.IsDeleted = false;
                e.DeletedBy = null;
                e.DeletedAt = null;
            });
            await _OrganizationService.EditRangeAsync(Organizations);
            return Ok();
        }

        [HttpGet]
        [Route("{currentPage}/{pageSize}")]
        [CustomAuthorizationFilter("Organization")]
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
            PagedListModel<Organization> pagedList = new PagedListModel<Organization>(currentPage, pageSize);
            pagedList.QueryOptions.SortField = sortBy ?? pagedList.QueryOptions.SortField;
            pagedList.QueryOptions.SearchOrder = sortOrder ?? pagedList.QueryOptions.SearchOrder;

            pagedList.DataList = await 
                _OrganizationService.FindAsync(pagedList.QueryOptions, 
                c => c.IsDeleted == isDeleted && 
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
        [CustomAuthorizationFilter("Organization")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> Get(int id)
        {
            Organization Organization = (await _OrganizationService.FindAsync(c => c.Id == id)).SingleOrDefault();
            return Ok(Organization);
        }

        [HttpGet]
        [Route("GetItemList/{lang}")]
        [ApiExplorerSettings(GroupName = "Admin")]
        [AllowAnonymous]
        public async Task<ActionResult> GetItemList(string lang)
        {
            bool isArabic = lang.Equals("ar", StringComparison.OrdinalIgnoreCase);
            IEnumerable<SelectItemModel> items = (await _OrganizationService.GetAllAsync())
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
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> GetNextCode()
        {
            int nextCode = (await _OrganizationService.MaxAsync<int>(c => c.Code)) + 1;

            return Ok(new
            {
                nextCode
            });
        }

        #endregion
    }
}