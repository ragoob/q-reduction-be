using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QReduction.Apis.Infrastructure;
using QReduction.Apis.Models;
using QReduction.Apis;
using QReduction.Core.Domain.Acl;
using QReduction.Core.Models;
using QReduction.Core.Service.Custom;
using QReduction.Core.Service.Generic;
using QReduction.Api;

namespace QReduction.Apis.Controllers.Acl
{
    [Route("api/[controller]")]
    [ApiController]
   // [EnableCors("QReductionPolicy")]
    [ValidateModelFilter]
    public class RoleController : CustomBaseController
    {


        #region Fields
        private readonly IRoleService _roleService;
        private readonly IService<Page> _pageService;
        private readonly IService<PagePermission> _pagePermissionService;
        private readonly IService<RolePagePermission> _rolePagePermissionService;
        #endregion

        #region ctor

        public RoleController(
            IRoleService roleService,
             IService<Page> pageService,
             IService<PagePermission> pagePermissionService,
              IService<RolePagePermission> rolePagePermissionService)
        {
            _roleService = roleService;
            _pageService = pageService;
            _pagePermissionService = pagePermissionService;
            _rolePagePermissionService = rolePagePermissionService;
        }

        #endregion

        #region Actions
        [HttpPost]
        // [CustomAuthorizationFilter("Role.Add")]
        [ApiExplorerSettings(GroupName = "Admin")]

        public async Task<IActionResult> Post([FromBody] RoleModel model)
        {

            if (await _roleService.AnyAsync(info => info.Code == model.Code))
                return BadRequest(Messages.Exist_Code);

            if (await _roleService.AnyAsync(info => info.NameAr == model.NameAr))
                return BadRequest(Messages.Exist_NameAr);

            if (await _roleService.AnyAsync(info => info.NameEn == model.NameEn))
                return BadRequest(Messages.Exist_NameEn);

            Role role = new Role
            {
                Id = model.Id,
                NameAr = model.NameAr,
                NameEn = model.NameEn,
                ReadOnly = model.ReadOnly,
                Code = model.Code,
                CreateAt = DateTime.UtcNow,
                CreateBy = UserId
            };

            List<SystemPageVM> Pages = new List<SystemPageVM>();

            model.ApplicationPages.ForEach(p =>
            {
                Pages.AddRange(p.SystemPages);
            });
            List<RolePagePermission> rolePagePermissions = GetRolePagePermissions(Pages);
            await _roleService.AddWithPermissionsAsync(role, rolePagePermissions);
            return Ok();

        }

        [HttpPut]
        //[CustomAuthorizationFilter("Role.Edit")]
        [ApiExplorerSettings(GroupName = "Admin")]

        public async Task<IActionResult> Put([FromBody]RoleModel model)
        {

            if (await _roleService.AnyAsync(info => info.Code == model.Code && info.Id != model.Id))
                return BadRequest(Messages.Exist_Code);

            if (await _roleService.AnyAsync(info => info.NameAr == model.NameAr && info.Id != model.Id))
                return BadRequest(Messages.Exist_NameAr);

            if (await _roleService.AnyAsync(info => info.NameEn == model.NameEn && info.Id != model.Id))
                return BadRequest(Messages.Exist_NameEn);

            Role role = await _roleService.GetByIdAsync(model.Id);

            if (role == null)
                return NotFound();

            role.Id = model.Id;
            role.NameAr = model.NameAr;
            role.NameEn = model.NameEn;
            role.ReadOnly = model.ReadOnly;
            role.CreateBy = model.CreateBy;
            role.CreateAt = model.CreateAt;
            role.UpdateAt = DateTime.UtcNow;
            role.UpdateBy = UserId;
            role.Code = model.Code;
            role.IsDeleted = model.IsDeleted;
            role.DeletedBy = model.DeletedBy;
            role.DeletedAt = model.DeletedAt;


            List<SystemPageVM> Pages = new List<SystemPageVM>();

            model.ApplicationPages.ForEach(p =>
            {
                Pages.AddRange(p.SystemPages);
            });
            List<RolePagePermission> rolePagePermissions = GetRolePagePermissions(Pages);

            await _roleService.UpdateWithPermissionsAsync(role, rolePagePermissions);
            return Ok();
        }


        [HttpPut]
        [Route("RemoveToRecycleBin/{id}")]
        // [CustomAuthorizationFilter("Role.Delete")]
        [ApiExplorerSettings(GroupName = "Admin")]

        public async Task<IActionResult> RemoveToRecycleBin(int id)
        {
            Role role = await _roleService.GetByIdAsync(id);

            if (role != null)
            {
                role.DeletedAt = DateTime.UtcNow;
                role.DeletedBy = UserId;
                role.IsDeleted = true;
                await _roleService.EditAsync(role);
            }

            return Ok();
        }


        [HttpPut]
        [Route("RestoreDeleted/{id}")]
        [ApiExplorerSettings(GroupName = "Admin")]

        public async Task<IActionResult> RestoreDeleted(int id)
        {
            var entity = await _roleService.GetByIdAsync(id);
            if (entity != null)
            {
                entity.DeletedAt = null;
                entity.DeletedBy = null;
                entity.IsDeleted = false;
                await _roleService.EditAsync(entity);
            }
            return Ok();
        }



        [HttpGet]
        // [CustomAuthorizationFilter("Role")]
        [Route("{currentPage}/{pageSize}")]
        [ApiExplorerSettings(GroupName = "Admin")]

        public async Task<IActionResult> Get(int currentPage, int pageSize,
             [FromQuery]int? code, [FromQuery] string nameAr, [FromQuery] string nameEn, [FromQuery] bool isDeleted)
        {
            PagedListModel<RoleModel> pagedList = new PagedListModel<RoleModel>(currentPage, pageSize);

            List<Role> rolesList = (await _roleService.FindAsync(pagedList.QueryOptions, c => c.IsDeleted == isDeleted
            && (code == null || c.Code == code) && (nameAr == null || c.NameAr.Contains(nameAr))
            && (nameEn == null || c.NameEn.Contains(nameEn)))).ToList();

            if (rolesList != null && rolesList.Count > 0)
                pagedList.DataList =
                    rolesList.Select(r =>
                    new RoleModel
                    {
                        Id = r.Id,
                        NameAr = r.NameAr,
                        NameEn = r.NameEn,
                        ReadOnly = r.ReadOnly,
                        IsDeleted = r.IsDeleted,
                        CreateAt = r.CreateAt,
                        CreateBy = r.CreateBy,
                        UpdateAt = r.UpdateAt,
                        UpdateBy = r.UpdateBy,
                        Code = r.Code,
                        DeletedAt = r.DeletedAt,
                        DeletedBy = r.DeletedBy,


                    }
                );



            return Ok(pagedList);
        }

        [HttpGet("{id}")]
        //[CustomAuthorizationFilter("Role")]
        [ApiExplorerSettings(GroupName = "Admin")]

        public async Task<IActionResult> Get(int id)
        {
            Role role = await _roleService.GetByIdAsync(id);

            if (role == null)
                return NotFound();

            RoleModel roleModel = new RoleModel
            {
                Id = role.Id,
                NameAr = role.NameAr,
                NameEn = role.NameEn,
                ReadOnly = role.ReadOnly,
                IsDeleted = role.IsDeleted,
                CreateAt = role.CreateAt,
                CreateBy = role.CreateBy,
                UpdateAt = role.UpdateAt,
                UpdateBy = role.UpdateBy,
                Code = role.Code,
                DeletedAt = role.DeletedAt,
                DeletedBy = role.DeletedBy,

                ApplicationPages = await GetRolePagesWithPermissions(role.Id)
            };

            return Ok(roleModel);
        }

        [HttpGet]
        [Route("GetEmptySystemPages")]
        //[CustomAuthorizationFilter("Role")]
        [ApiExplorerSettings(GroupName = "Admin")]

        public async Task<IActionResult> GetEmptySystemPages()
        {
            List<ApplicationPagesViewModel> ApplicationPages = await GetRolePagesWithPermissions(0);
            return Ok(ApplicationPages);
        }

        [HttpGet]
        [Route("GetItemList/{lang}")]
        [AllowAnonymous]
        [ApiExplorerSettings(GroupName = "Admin")]

        public async Task<ActionResult> GetItemList(string lang)
        {
            bool isArabic = lang.Equals("ar", StringComparison.OrdinalIgnoreCase);
            IEnumerable<SelectItemModel> items = (await _roleService.GetAllAsync())
                .Select(info =>
                    new SelectItemModel
                    {
                        Value = info.Id,
                        Label = info.NameAr
                    });

            return Ok(items);
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("GetNextCode")]
        [ApiExplorerSettings(GroupName = "Admin")]

        public async Task<IActionResult> GetNextCode()
        {
            int nextCode = (await _roleService.MaxAsync<int>(c => c.Code)) + 1;
            return Ok(new
            {
                nextCode
            });
        }
        #endregion

        #region Methods

        private async Task<List<ApplicationPagesViewModel>> GetRolePagesWithPermissions(int roleId)
        {
            List<SystemPageVM> systemPages = (await _pageService.GetAllAsync("Application"))?
                .Select(p => new SystemPageVM
                {
                    Id = p.Id,
                    NameAr = p.NameAr,
                    NameEn = p.NameEn,
                })
                .ToList();




            List<PagePermission> pagePermissions = (await _pagePermissionService.GetAllAsync("PermissionsTerm")).ToList();
            List<RolePagePermission> rolePagePermissions = (await _rolePagePermissionService.FindAsync(up => up.RoleId == roleId)).ToList();


            systemPages.ForEach(p =>
            {
                p.PagePermissions = pagePermissions.Where(pp => pp.PageId == p.Id)
                                        .Select(pp => new SystemPagePermissionVM
                                        {
                                            Id = pp.Id,
                                            PermissionTermId = pp.PermissionTermId,
                                            PermissionTermNameAr = pp.PermissionsTerm.NameAr,
                                            PermissionTermNameEn = pp.PermissionsTerm.NameEn,
                                            Included = rolePagePermissions.Any(upp => upp.PagePermissionId == pp.Id)
                                        });
            });

            List<ApplicationPagesViewModel> applicationPages = new List<ApplicationPagesViewModel> 
            { 
                new ApplicationPagesViewModel 
                {
                    AppId = 1,
                    AppNameAr = "QReduction",
                    AppNameEn = "QReduction",
                    SystemPages = systemPages

                }
            };

            return applicationPages;
        }
        private List<RolePagePermission> GetRolePagePermissions(List<SystemPageVM> systemPages)
        {
            List<RolePagePermission> rolePagePermissions = new List<RolePagePermission>();

            systemPages.ForEach(p =>
            {
                rolePagePermissions.AddRange(p.PagePermissions.Where(pp => pp.Included).Select(pp => new RolePagePermission
                {
                    PagePermissionId = pp.Id,
                }));

            });

            return rolePagePermissions;
        }
        #endregion


    }
}