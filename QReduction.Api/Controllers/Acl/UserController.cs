using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using QReduction.Apis.Controllers;
using QReduction.Apis.Infrastructure;
using QReduction.Core.Domain;
using QReduction.Core.Models;
using QReduction.Core.Service.Custom;
using QReduction.Core.Service.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QReduction.Core.Domain.Acl;
using QReduction.Apis;
using QReduction.Apis.Models;
using QReduction.Api;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace QReduction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableCors("QReductionPolicy")]
    [ValidateModelFilter]
    public class UserController : CustomBaseController
    {
        #region Fields
        private readonly IWebHostEnvironment _environment;
        private readonly IUserService _userService;
        private readonly IEncryptionProvider _encryptionProvider;
        private readonly IEmailSender _emailSender;
        private readonly IService<UserRole> _userRoleService;
        //private readonly IService<SystemPage> _systemPageService;
        //private readonly IService<SystemPagePermission> _systemPagePermissionService;
        //private readonly IService<UserPagePermission> _userPagePermissionService;

        #endregion

        #region ctor

        public UserController(IUserService userService, IEncryptionProvider encryptionProvider,
            IService<UserRole> userRoleService,
            IWebHostEnvironment environment,
            IEmailSender emailSender
            )
        {
            _userService = userService;
            _encryptionProvider = encryptionProvider;
            _userRoleService = userRoleService;
            _emailSender = emailSender;
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            //_systemPageService = systemPageService;
            //_systemPagePermissionService = systemPagePermissionService;
            //_userPagePermissionService = userPagePermissionService;
        }

        #endregion
        #region Actions superAdmin
        private string GeneratePassword(int length = 6)
        {
            Random random = new Random();
            string password = string.Empty;
            length = length <= 0 ? 6 : length;

            for (int i = 0; i < length; i++)
            {
                password += random.Next(0, 9);
            }
            return password;
        }

        [HttpPost]
        [CustomAuthorizationFilter("Users.Add")]
        [ApiExplorerSettings(GroupName = "SuperAdmin")]
        [Route("AddOrganizationAdmin")]
        public async Task<IActionResult> AddOrganizationAdmin(UserModel model)
        {
            if (await _userService.AnyAsync(u => u.PhoneNumber.Equals(model.PhoneNumber) || u.Email.Equals(model.Email, StringComparison.OrdinalIgnoreCase)))
                return BadRequest(Messages.Exists_EmailOrPhone);
            var _password = $"{GeneratePassword()}";

            _encryptionProvider.CreatePasswordHash(_password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = new User
            {
                Email = model.Email,
                IsActive = model.IsActive,
                PhoneNumber = model.PhoneNumber,
                UserGuid = Guid.NewGuid(),
                UserName = model.Name,
                Password = passwordHash,
                PasswordSalt = passwordSalt,
                UserTypeId = UserTypes.OrganizationAdmin,//(UserTypes)model.UserTypeId,
                RowGuid = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                CreatedBy = UserId,
                LastUpdateDate = DateTime.UtcNow,
                OrganizationId = model.OrganizationId,
                IsFirstLogin = true
            };

            await _userService.AddWithDetailsAsync(user, model.UserRoles);
            try
            {
                await _emailSender.SendMail(new string[] { user.Email }, "كلمه المرور", $" {_password} ");

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok();
        }


        //[HttpGet]
        //[CustomAuthorizationFilter("Users")]
        //[Route("{currentPage}/{pageSize}")]
        //public async Task<IActionResult> Get(int currentPage, int pageSize)
        //{
        //    PagedListModel<UserModel> pagedList = new PagedListModel<UserModel>(currentPage, pageSize);

        //    List<User> usersList = (await _userService.GetAsync(pagedList.QueryOptions)).ToList();

        //    if (usersList != null && usersList.Count > 0)
        //        pagedList.DataList =
        //            usersList.Select(u =>
        //            new UserModel
        //            {
        //                Email = u.Email,
        //                Name = u.UserName,
        //                IsActive = u.IsActive,
        //                Id = u.Id,
        //                PhoneNumber = u.PhoneNumber,
        //                UserTypeId = UserTypes.AdminUser
        //            }
        //        );

        //    return Ok(pagedList);
        //}

        #endregion


        #region actions organizationAdmin
        [HttpPost]
        [ApiExplorerSettings(GroupName = "OrganizationAdmin")]
        [Route("AddOrganizationUser")]
        public async Task<IActionResult> AddOrganizationUser(UserModel model)
        {
            if (await _userService.AnyAsync(u => u.PhoneNumber.Equals(model.PhoneNumber) || u.Email.Equals(model.Email, StringComparison.OrdinalIgnoreCase)))
                return BadRequest(Messages.Exists_EmailOrPhone);
            var _password = $"{GeneratePassword()}";
            _encryptionProvider.CreatePasswordHash(_password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = new User
            {
                Email = model.Email,
                IsActive = model.IsActive,
                PhoneNumber = model.PhoneNumber,
                UserGuid = Guid.NewGuid(),
                UserName = model.Name,
                Password = passwordHash,
                PasswordSalt = passwordSalt,
                UserTypeId = (UserTypes)model.UserTypeId, // shift subervisor || tailor
                RowGuid = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                CreatedBy = UserId,
                LastUpdateDate = DateTime.UtcNow,
                OrganizationId = OrganizationId,
                BranchId = model.branchId,
                IsFirstLogin = true
            };

            await _userService.AddWithDetailsAsync(user, model.UserRoles);
            try
            {
                await _emailSender.SendMail(new string[] { user.Email }, "كلمه المرور", $" {_password} ");

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Ok();
        }

        [HttpGet]
        [CustomAuthorizationFilter("Users")]
        [Route("GetOrganzationUsers/{currentPage}/{pageSize}")]
        [ApiExplorerSettings(GroupName = "OrganizationAdmin")]
        public async Task<IActionResult> GetOrganzationUsers(int currentPage, int pageSize,
           [FromQuery] string name,
           [FromQuery] string email,
           [FromQuery] string phoneNumber)
        {
            PagedListModel<UserModel> pagedList = new PagedListModel<UserModel>(currentPage, pageSize);

            List<User> usersList = (await _userService.FindAsync(pagedList.QueryOptions, c => c.OrganizationId == OrganizationId &&
             (name == null || c.UserName.Contains(name)) && (email == null || c.Email.Contains(email))
            && (phoneNumber == null || c.PhoneNumber.Contains(phoneNumber)))).ToList();

            if (usersList != null && usersList.Count > 0)
                pagedList.DataList =
                    usersList.Select(u =>
                    new UserModel
                    {
                        Email = u.Email,
                        Name = u.UserName,
                        IsActive = u.IsActive,
                        Id = u.Id,
                        PhoneNumber = u.PhoneNumber
                    }
                );

            return Ok(pagedList);
        }
        #endregion


        #region globale func
        [HttpPost]
        [Route("UpdateUser")]
        // [CustomAuthorizationFilter("Users.Edit")]
        public async Task<IActionResult> UpdateUser([FromForm] UserModel model)
        {
            if (await _userService.AnyAsync
                (u =>
                    model.Id != u.Id &&
                    (u.PhoneNumber.Equals(model.PhoneNumber) || u.Email.Equals(model.Email, StringComparison.OrdinalIgnoreCase))
                )
               ) return BadRequest(Messages.Exists_EmailOrPhone);

            User user = await _userService.GetByIdAsync(model.Id);

            if (user == null)
                return NotFound();


            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                _encryptionProvider.CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);
                user.Password = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            if (model.UserImage != null)
            {
                var uploads = Path.Combine(_environment.WebRootPath, "UserUploads");
                var imgPath = Path.Combine(uploads, user.UserGuid.ToString() + model.UserImage.FileName);
                if (model.UserImage.Length > 0)
                {
                    using (var fileStream = new FileStream(imgPath, FileMode.Create))
                    {
                        await model.UserImage.CopyToAsync(fileStream);
                    }
                }
                user.PhotoPath = "UserUploads/" + user.UserGuid.ToString() + model.UserImage.FileName;
            }
            user.Email = string.IsNullOrEmpty(model.Email) ? user.Email : model.Email;
            user.PhoneNumber = string.IsNullOrEmpty(model.PhoneNumber) ? user.PhoneNumber : model.PhoneNumber;
            user.UserName = model.Name;
            user.UserTypeId = user.UserTypeId;
            user.UpdatedBy = UserId;
            user.UpdatedAt = user.LastUpdateDate = DateTime.UtcNow;
            user.IsActive = true;
            user.IsVerified = true;
            user.IsFirstLogin = false;
            await _userService.UpdateWithDetailsAsync(user, model.UserRoles);
            return Ok();
        }

        [HttpGet("{id}")]
        [CustomAuthorizationFilter("Users")]
        public async Task<IActionResult> Get(int id)
        {
            User user = await _userService.GetByIdAsync(id);
            List<UserRole> userRoles = (await _userRoleService.FindAsync(c => c.UserId == id)).ToList();

            if (user == null)
                return NotFound();

            UserModel userModel = new UserModel
            {
                Email = user.Email,
                Name = user.UserName,
                IsActive = user.IsActive,
                Id = user.Id,
                UserTypeId = user.UserTypeId,
                PhoneNumber = user.PhoneNumber,
                UserRoles = userRoles,
                PhotoPath = user.PhotoPath,
                OrganizationId = user.OrganizationId
            };

            return Ok(userModel);
        }

        [HttpGet("GetUserbyGuid/{guid}")]
        // [CustomAuthorizationFilter("Users")]
        public async Task<IActionResult> Get(string guid)
        {
            IEnumerable<User> users = await _userService.FindAsync(u => u.UserGuid.ToString() == guid);

            if (users == null)
                return NotFound();

            var user = users.FirstOrDefault();
            UserModel userModel = new UserModel
            {
                Email = user.Email,
                Name = user.UserName,
                IsActive = user.IsActive,
                Id = user.Id,
                UserTypeId = user.UserTypeId,
                PhoneNumber = user.PhoneNumber,
                PhotoPath = user.PhotoPath,
                //UserRoles = userRoles,
                OrganizationId = user.OrganizationId
            };

            return Ok(userModel);
        }


        //[HttpGet]
        ////  [ApiExplorerSettings(GroupName = "Admin")]
        //public async Task<IActionResult> GetUsers(string ids)
        //{
        //    //TODO : Create cutom method on service to get only username and Ids ...

        //    int[] usersIds = ids.Split(',').Select(Int32.Parse).ToArray();
        //    List<User> users = (await _userService.FindAsync(u => usersIds.Contains(u.Id))).ToList();

        //    if (users == null)
        //        return BadRequest(Messages.ThereUsersNotFound);

        //    return Ok(users);
        //}


        [HttpGet]
        [CustomAuthorizationFilter("Users")]
        [Route("{currentPage}/{pageSize}")]
        [ApiExplorerSettings(GroupName = "Admin")]

        public async Task<IActionResult> Get(int currentPage, int pageSize,
            [FromQuery] string name,
            [FromQuery] string email,
            [FromQuery] string phoneNumber)
        {
            try
            {
                PagedListModel<UserModel> pagedList = new PagedListModel<UserModel>(currentPage, pageSize);

                if (UserId == default(int))
                    return Unauthorized();
                var currentUserTypeId = (await _userService.GetByIdAsync(UserId)).UserTypeId;



                List<User> usersList = currentUserTypeId == UserTypes.OrganizationAdmin ?
                    (await _userService.FindAsync(pagedList.QueryOptions, c => c.UserTypeId != UserTypes.Mobile && c.OrganizationId == OrganizationId &&
                 (name == null || c.UserName.Contains(name)) && (email == null || c.Email.Contains(email))
                && (phoneNumber == null || c.PhoneNumber.Contains(phoneNumber)), "Organization" , "Branch")).ToList() :

                currentUserTypeId == UserTypes.SuperAdmin ?

                (await _userService.FindAsync(pagedList.QueryOptions, c => c.UserTypeId != UserTypes.Mobile &&
                (name == null || c.UserName.Contains(name)) && (email == null || c.Email.Contains(email))
               && (phoneNumber == null || c.PhoneNumber.Contains(phoneNumber)), "Organization", "Branch")).ToList() : null
               ;

                if (usersList != null && usersList.Count > 0)
                    pagedList.DataList =
                        usersList.Select(u =>
                        new UserModel
                        {
                            Email = u.Email,
                            Name = u.UserName,
                            IsActive = u.IsActive,
                            Id = u.Id,
                            PhoneNumber = u.PhoneNumber,
                            OrganizationId = u.OrganizationId,
                            branchId = u.OrganizationId,
                            UserTypeId = u.UserTypeId
                           
                             
                        }
                    );

                return Ok(pagedList);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        [Authorize]
        [Route("GetCurrentUserProfile")]
        [ApiExplorerSettings(GroupName = "Admin")]

        public async Task<IActionResult> Get()
        {
            User user = (await _userService.FindAsync(c => c.Id == UserId, "UserType")).SingleOrDefault();
            UserProfileModel profile = ShallowMap<UserProfileModel>(user);
            return Ok(profile);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put(UserProfileModel model)
        {
            User user = await _userService.GetByIdAsync(model.Id);

            if (await _userService.AnyAsync(info => info.Email == model.Email && info.Id != model.Id))
                return BadRequest(Messages.Exists_Email);
            if (await _userService.AnyAsync(info => info.PhoneNumber == model.PhoneNumber && info.Id != model.Id))
                return BadRequest(Messages.Exists_Phone);
            if (await _userService.AnyAsync(info => info.UserName == model.UserName && info.Id != model.Id))
                return BadRequest(Messages.Exists_UserName);
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.PhotoPath = model.PhotoPath;
            user.UserName = model.UserName;
            user.LastUpdateDate = DateTime.UtcNow;
            user.UpdatedBy = UserId;
            await _userService.EditAsync(user);
            return Ok();
        }

        //[HttpPut]
        //[Route("ChangePassword")]
        //[Authorize]
        //public async Task<IActionResult> Put(ChangePasswordModel model)
        //{
        //    User user = await _userService.GetByIdAsync(UserId);
        //    if (model.NewPasswordConfirmation != model.NewPassword)
        //        return BadRequest(Messages.NewPasswordError);
        //    _encryptionProvider.CreatePasswordHash(model.OldPassword, out byte[] passwordHashOld, out byte[] passwordSaltOld);
        //    if (passwordHashOld != user.Password)
        //        return BadRequest(Messages.OldPasswordError);
        //    _encryptionProvider.CreatePasswordHash(model.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);
        //    user.Password = passwordHash;
        //    user.PasswordSalt = passwordSalt;
        //    user.LastUpdateDate = DateTime.UtcNow;
        //    user.UpdatedBy = UserId;
        //    await _userService.EditAsync(user);
        //    return Ok();
        //}

        #endregion

        //      #region Actions Commented

        //      [HttpPost]
        //      [CustomAuthorizationFilter("Users.Add")]
        //      public async Task<IActionResult> Post(UserModel model)
        //      {
        //          if (await _userService.AnyAsync(u => u.PhoneNumber.Equals(model.PhoneNumber) || u.Email.Equals(model.Email, StringComparison.OrdinalIgnoreCase)))
        //              return BadRequest(Messages.Exists_EmailOrPhone);

        //          if (string.IsNullOrWhiteSpace(model.Password))
        //              model.Password = "ElShenawy";

        //          _encryptionProvider.CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);

        //          User user = new User
        //          {
        //              Email = model.Email,
        //              IsActive = model.IsActive,
        //              PhoneNumber = model.PhoneNumber,
        //              UserGuid = Guid.NewGuid(),
        //              UserName = model.Name,
        //              Password = passwordHash,
        //              PasswordSalt = passwordSalt,
        //              IsAdmin = model.IsAdmin,
        //              RowGuid = Guid.NewGuid(),
        //              CreatedAt = DateTime.UtcNow,
        //              CreatedBy = UserId,
        //              LastUpdateDate = DateTime.UtcNow
        //          };
        //          List<UserPagePermission> userPagePermissions = GetUserPagePermissions(model.SystemPages);
        //          await _userService.AddAsync(user, userPagePermissions);
        //          return Ok();
        //      }

        //      [HttpPut]
        //      [CustomAuthorizationFilter("Users.Edit")]
        //      public async Task<IActionResult> Put(UserModel model)
        //      {
        //          if (await _userService.AnyAsync
        //              (u => 
        //                  model.Id != u.Id &&
        //                  (u.PhoneNumber.Equals(model.PhoneNumber) || u.Email.Equals(model.Email, StringComparison.OrdinalIgnoreCase))   
        //              )
        //             )return BadRequest(Messages.Exists_EmailOrPhone);

        //          User user = await _userService.GetByIdAsync(model.Id);

        //          if (user == null)
        //              return NotFound();


        //          if (!string.IsNullOrWhiteSpace(model.Password))
        //          {
        //              _encryptionProvider.CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);
        //              user.Password = passwordHash;
        //              user.PasswordSalt = passwordSalt;
        //          }

        //          user.Email = model.Email;
        //          user.PhoneNumber = model.PhoneNumber;
        //          user.UserName = model.Name;
        //          user.IsAdmin = model.IsAdmin;
        //          user.UpdatedBy = UserId;
        //          user.UpdatedAt = user.LastUpdateDate = DateTime.UtcNow;
        //          user.IsActive = model.IsActive;

        //          List<UserPagePermission> userPagePermissions = GetUserPagePermissions(model.SystemPages);

        //          await _userService.EditAsync(user, userPagePermissions);
        //          return Ok();
        //      }

        //      [HttpGet]
        //      [CustomAuthorizationFilter("Users")]
        //      [Route("{currentPage}/{pageSize}")]
        //      public async Task<IActionResult> Get(int currentPage, int pageSize)
        //      {
        //          PagedListModel<UserModel> pagedList = new PagedListModel<UserModel>(currentPage, pageSize);

        //          List<User> usersList = (await _userService.GetAsync(pagedList.QueryOptions)).ToList();

        //          if (usersList != null && usersList.Count > 0)
        //              pagedList.DataList = 
        //                  usersList.Select(u => 
        //                  new UserModel
        //                  {
        //                      Email = u.Email,
        //                      Name = u.UserName,
        //                      IsActive = u.IsActive,
        //                      Id = u.Id,
        //                      IsAdmin = u.IsAdmin,
        //                      PhoneNumber = u.PhoneNumber
        //                  }
        //              );

        //          return Ok(pagedList);
        //      }

        //[HttpGet("{id}")]
        //      [CustomAuthorizationFilter("Users")]
        //      public async Task<IActionResult> Get(int id)
        //      {
        //          User user = await _userService.GetByIdAsync(id);

        //          if (user == null)
        //              return NotFound();

        //          UserModel userModel = new UserModel
        //          {
        //              Email = user.Email,
        //              Name = user.UserName,
        //              IsActive = user.IsActive,
        //              Id = user.Id,
        //              IsAdmin = user.IsAdmin,
        //              PhoneNumber = user.PhoneNumber,
        //              SystemPages = await GetUserPagesWithPermissions(user.Id)
        //          };

        //          return Ok(userModel);
        //      }

        //      [HttpGet]
        //      [Route("GetEmptySystemPages")]
        //      [CustomAuthorizationFilter("Users")]
        //      public async Task<IActionResult> GetEmptySystemPages()
        //      {
        //          List<SystemPageVM> systemPages = await GetUserPagesWithPermissions(0);
        //          return Ok(systemPages);
        //      }

        //      [HttpGet]
        //      [Route("GetItemList/{lang}")]
        //      [AllowAnonymous]
        //      public async Task<ActionResult> GetItemList(string lang)
        //      {
        //          bool isArabic = lang.Equals("ar", StringComparison.OrdinalIgnoreCase);
        //          IEnumerable<SelectItemModel> items = (await _userService.GetAllAsync())
        //              .Select(info => 
        //                  new SelectItemModel
        //                  {
        //                      Value = info.Id, Label = info.UserName
        //                  });

        //          return Ok(items);
        //      }

        //      #endregion

        //      #region Methods

        //      private async Task<List<SystemPageVM>> GetUserPagesWithPermissions(int userId)
        //      {
        //          List<SystemPageVM> systemPages = (await _systemPageService.GetAllAsync())?
        //              .Select(p => ShallowMap<SystemPageVM>(p))
        //              .ToList();

        //          List<SystemPagePermission> pagePermissions = (await _systemPagePermissionService.GetAllAsync("SystemPermissionTerm")).ToList();
        //          List<UserPagePermission> userPagePermissions = (await _userPagePermissionService.FindAsync(up => up.UserId == userId)).ToList();


        //          systemPages.ForEach(p =>
        //          {
        //              p.PagePermissions = pagePermissions.Where(pp => pp.PageId == p.Id)
        //                                      .Select(pp => new SystemPagePermissionVM
        //                                      {
        //                                          Id = pp.Id,
        //                                          PermissionTermId = pp.PermissionTermId,
        //                                          PermissionTermNameAr = pp.SystemPermissionTerm.NameAr,
        //                                          PermissionTermNameEn = pp.SystemPermissionTerm.NameEn,
        //                                          Included = userPagePermissions.Any(upp => upp.PagePermissionId == pp.Id)
        //                                      });
        //          });

        //          return systemPages;
        //      }

        //      private List<UserPagePermission> GetUserPagePermissions(List<SystemPageVM> systemPages)
        //      {
        //          List<UserPagePermission> userPagePermissions = new List<UserPagePermission>();

        //          systemPages.ForEach(p =>
        //          {
        //              userPagePermissions.AddRange(p.PagePermissions.Where(pp => pp.Included).Select( pp =>  new UserPagePermission
        //              {
        //                  PagePermissionId = pp.Id,
        //              }));

        //          });

        //          return userPagePermissions;
        //      }

        //      #endregion






    }
}