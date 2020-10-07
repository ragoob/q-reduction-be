using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using QReduction.Api;
using QReduction.Apis.Controllers;
using QReduction.Apis.Infrastructure;
using QReduction.Apis.Models;
using QReduction.Core.Domain;
using QReduction.Core.Infrastructure;
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
    [EnableCors("QReductionPolicy")]
    [ValidateModelFilter]
    [ApiExplorerSettings(GroupName = "Admin")]
    public class ServiceController : CustomBaseController
    {
        #region Fields
        private readonly IService<Service> _serviceService;
        private readonly IService<BranchService> _branchServiceService;
        #endregion

        #region ctor
        public ServiceController(IService<Service> serviceService, IService<BranchService> branchServiceService)
        {
            _serviceService = serviceService;
            _branchServiceService = branchServiceService;
        }
        #endregion

        #region Actions services

        [HttpPost]
        [CustomAuthorizationFilter("Service.Add")]
        [ApiExplorerSettings(GroupName = "OrganizationAdmin")]
        public async Task<IActionResult> Post(Service service)
        {

            if (await _serviceService.AnyAsync(info => info.Code == service.Code))
                return BadRequest(Messages.Exist_Code);

            if (await _serviceService.AnyAsync(info => info.NameAr == service.NameAr))
                return BadRequest(Messages.Exist_NameAr);

            if (await _serviceService.AnyAsync(info => info.NameEn == service.NameEn))
                return BadRequest(Messages.Exist_NameEn);

            service.CreateAt = DateTime.UtcNow;
            service.CreateBy = UserId;
            service.OrganizationId = OrganizationId;
            if (OrganizationId == 0)
                return BadRequest("Organization Not Defined For Logged User");
            await _serviceService.AddAsync(service);
            return Ok();
        }

        [HttpPut]
        [CustomAuthorizationFilter("Service.Edit")]
        [ApiExplorerSettings(GroupName = "OrganizationAdmin")]
        public async Task<IActionResult> Put(Service service)
        {

            if (await _serviceService.AnyAsync(info => info.Code == service.Code && info.Id != service.Id))
                return BadRequest(Messages.Exist_Code);

            if (await _serviceService.AnyAsync(info => info.NameAr == service.NameAr && info.Id != service.Id))
                return BadRequest(Messages.Exist_NameAr);

            if (await _serviceService.AnyAsync(info => info.NameEn == service.NameEn && info.Id != service.Id))
                return BadRequest(Messages.Exist_NameEn);

            service.UpdateAt = DateTime.UtcNow;
            service.UpdateBy = UserId;
            await _serviceService.EditAsync(service);
            return Ok();
        }

        [HttpPut]
        [Route("RemoveToRecycleBin/{id}")]
        [CustomAuthorizationFilter("Service.Delete")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> RemoveToRecycleBin(int id)
        {
            Service service = await _serviceService.GetByIdAsync(id);

            if (service != null)
            {
                service.DeletedAt = DateTime.UtcNow;
                service.DeletedBy = UserId;
                service.IsDeleted = true;
                await _serviceService.EditAsync(service);
            }

            return Ok();
        }

        [HttpPut]
        [Route("RestoreDeleted/{id}")]
        [CustomAuthorizationFilter("Service.Restore")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> RestoreDeleted(int id)
        {
            var entity = await _serviceService.GetByIdAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = false;
                entity.DeletedBy = null;
                entity.DeletedAt = null;
                await _serviceService.EditAsync(entity);
            }
            return Ok();
        }

        [HttpPut]
        [Route("MultiRemoveToRecycleBin/{ids}")]
        [CustomAuthorizationFilter("Service.Delete")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> MultiRemoveToRecycleBin(string ids)
        {
            int[] idsToDelete = ids.Split(',').Select(Int32.Parse).ToArray();

            List<Service> services = _serviceService.Find(item => idsToDelete.Contains(item.Id)).ToList();
            if (services == null)
                return BadRequest(Messages.CanNotDeleteMulti);
            services.ForEach(e =>
            {
                e.DeletedAt = DateTime.UtcNow;
                e.DeletedBy = UserId;
                e.IsDeleted = true;
            });

            await _serviceService.EditRangeAsync(services);
            return Ok();
        }

        [HttpPut]
        [Route("MultiRestoreDeleted/{ids}")]
        [CustomAuthorizationFilter("Service.Restore")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> MultiRestoreDeleted(string ids)
        {
            int[] idsToRestor = ids.Split(',').Select(Int32.Parse).ToArray();

            List<Service> services = _serviceService.Find(item => idsToRestor.Contains(item.Id)).ToList();
            if (services == null)
                return BadRequest(Messages.CanNotRestorMulti);
            services.ForEach(e =>
            {
                e.IsDeleted = false;
                e.DeletedBy = null;
                e.DeletedAt = null;
            });
            await _serviceService.EditRangeAsync(services);
            return Ok();
        }

        [HttpGet]
        [Route("{currentPage}/{pageSize}")]
        [CustomAuthorizationFilter("Service")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> Get(int currentPage, int pageSize,
            [FromQuery] string sortBy,[FromQuery] SearchOrders? sortOrder,[FromQuery] string searchText,
            [FromQuery] int? code,[FromQuery] string nameAr,[FromQuery] string nameEn, [FromQuery] bool isDeleted)
        {
            PagedListModel<Service> pagedList = new PagedListModel<Service>(currentPage, pageSize);
            pagedList.QueryOptions.SortField = sortBy ?? pagedList.QueryOptions.SortField;
            pagedList.QueryOptions.SearchOrder = sortOrder ?? pagedList.QueryOptions.SearchOrder;

            pagedList.DataList = await 
                _serviceService.FindAsync(pagedList.QueryOptions, 
                c => c.IsDeleted == isDeleted && c.OrganizationId==OrganizationId&&
                (code == null || c.Code == code) &&
                (nameAr == null || c.NameAr.Contains(nameAr)) &&
                (nameEn == null || c.NameEn.Contains(nameEn)) && 
                (string.IsNullOrWhiteSpace(searchText) || 
                    (c.Code.ToString().Contains(searchText) || (c.NameAr.Contains(searchText) || (c.NameEn.Contains(searchText))) ) )
                );

            return Ok(pagedList);
        }

        [HttpGet("{id}")]
        [CustomAuthorizationFilter("Service")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> Get(int id)
        {
            Service service = (await _serviceService.FindAsync(c => c.Id == id)).SingleOrDefault();
            return Ok(service);
        }

        [HttpGet]
        [Route("GetItemList/{lang}")]
        [AllowAnonymous]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<ActionResult> GetItemList(string lang)
        {
            bool isArabic = lang.Equals("ar", StringComparison.OrdinalIgnoreCase);
            IEnumerable<SelectItemModel> items = (await _serviceService.FindAsync(s=>s.OrganizationId==OrganizationId))
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
            int nextCode = (await _serviceService.MaxAsync<int>(c => c.Code) + 1);

            return Ok(new
            {
                nextCode
            });
        }

        #endregion

        #region servicesBranc
        [HttpPost]
        [Route("AddServicesToBranch")]
        [CustomAuthorizationFilter("Service.AddtoBranch")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> AddServicesToBranch(AssignServicesToBranch service)
        {

            var ListToRemove = _branchServiceService.Find(r => r.BranchId == service.BranchId);
            _branchServiceService.RemoveRange(ListToRemove);

            //if (await _branchServiceService.AnyAsync(info => info.BranchId == service.BranchId && service.ServicesIds.Contains( info.ServiceId)))
            //    return BadRequest(Messages.ServiceAlreadyAddedtoBranch);

            foreach (var item in service.ServicesIds)
            {
                await _branchServiceService.AddAsync(new BranchService() { BranchId = service.BranchId, ServiceId = item });
            }

           
            return Ok();
        }

      

        [HttpGet]
        [Route("GetOrganizationBranchServices")]
        [CustomAuthorizationFilter("Service.GetBranchService")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> GetOrganizationBranchServices(int branchId)
        {
            List<Service> services = new List<Service>();
            var query = (await _branchServiceService.FindAsync(a => a.BranchId == branchId, "Service")).ToList();

            services = query.Where(a=>a.Service.OrganizationId==OrganizationId).Select(a => a.Service).ToList();
            return Ok(services);
        }

        [HttpGet]
        [Route("GetOrganizationServices")]
        [CustomAuthorizationFilter("Service.GetBranchService")]
        [ApiExplorerSettings(GroupName = "Admin")]
        public async Task<IActionResult> GetOrganizationServices()
        {
            List<Service> services = new List<Service>();
             services = (await _serviceService.FindAsync(a => a.OrganizationId == OrganizationId)).ToList();

            //services = query.Where(a => a.Service.OrganizationId == OrganizationId).Select(a => a.Service).ToList();
            return Ok(services);
        }


        #endregion
        #region Mobile

        //[HttpGet]
        //[Route("GetServicesByBranch/{branchId}")]
        //[ApiExplorerSettings(GroupName = "Mobile")]
        //[Authorize(Roles = SystemKeys.MobileRole)]
        //public async Task<IActionResult> GetServicesByBranch(int branchId) 
        //{
        //   IEnumerable<Service> services = 
        //        (await _branchServiceService.FindAsync(bs => bs.BranchId == branchId,
        //        "Service")).Select(s => s.Service).Where(s => !s.IsDeleted);

        //    return Ok(services);
        //}


        #endregion
    }
}