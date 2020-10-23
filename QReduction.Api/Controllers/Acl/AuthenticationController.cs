using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using QReduction.Api;
using QReduction.Apis.Infrastructure;
using QReduction.Apis.Models;
using QReduction.Core.Domain.Acl;
using QReduction.Core.Domain.Acl.FacebookModels;
using QReduction.Core.Infrastructure;
using QReduction.Core.Service.Custom;
using QReduction.Services.Custom;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace QReduction.Apis.Controllers.Membership
{
    //LoginGmail
    [EnableCors("QReductionPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : CustomBaseController
    {

        #region fields
        private readonly IUserService _userService;
        private readonly IEncryptionProvider _encryptionProvider;
        private readonly IHostingEnvironment _environment;
        private readonly ITokenProvider _tokenProvider;
        //private readonly IService<UserPagePermission> _userPagePermissionService;
        //private readonly IService<SystemPagePermission> _systemPagePermissionService;
        private readonly Infrastructure.IEmailSender _emailSender;
        private readonly ISMSService _smsService;

        private readonly FacebookService _facebookService;

        #endregion

        #region ctor
        public AuthenticationController(
            IUserService userService,
            IEncryptionProvider encryptionProvider,
            ITokenProvider tokenProvider,
            IEmailSender emailSender,
            ISMSService smsService,
            IHostingEnvironment environment
            //IService<UserPagePermission> userPagePermissionService,
            //IService<SystemPagePermission> systemPagePermissionService
            )
        {
            _userService = userService;
            _encryptionProvider = encryptionProvider;
            _tokenProvider = tokenProvider;
            _emailSender = emailSender;
            _smsService = smsService;
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            //_userPagePermissionService = userPagePermissionService;
            //_systemPagePermissionService = systemPagePermissionService;
        }
        #endregion


        #region Actions SuperAdmin
       
        [HttpPost]
        [Route("SuperAdminRegister")]
        [AllowAnonymous]
        [ApiExplorerSettings(GroupName = "SuperAdmin")]
        public async Task<IActionResult> SuperAdminRegister(RegistrationModel model)
        {
            if (await _userService.AnyAsync(u =>
                (u.PhoneNumber == model.PhoneNumber || u.Email == model.Email)))
                return BadRequest(Messages.Exists_EmailOrPhone);

            _encryptionProvider.CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = new User
            {
                Email = model.Email,
                IsActive = true,
                PhoneNumber = model.PhoneNumber,
                UserGuid = Guid.NewGuid(),
                UserName = model.Email,
                Password = passwordHash,
                PasswordSalt = passwordSalt,
                LastLoginUtcDate = DateTime.UtcNow,
                UserTypeId = UserTypes.SuperAdmin
            };

            await _userService.AddAsync(user);

            return Ok();
        }

        #endregion


        #region Mobile

        [HttpPost]
        [Route("MobileRegister")]
        [AllowAnonymous]
        [ApiExplorerSettings(GroupName = "Mobile")]
        public async Task<IActionResult> MobileRegister(RegistrationModel model)
        {
            if (await _userService.AnyAsync(u =>
                (u.PhoneNumber == model.PhoneNumber || u.Email == model.Email)))
                return BadRequest(Messages.Exists_EmailOrPhone);

            _encryptionProvider.CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);
            User user = new User
            {
                Email = model.Email,
                IsActive = false,
                PhoneNumber = model.PhoneNumber,
                UserGuid = Guid.NewGuid(),
                UserName = model.Name,
                Password = passwordHash,
                PasswordSalt = passwordSalt,
                LastLoginUtcDate = DateTime.UtcNow,
                IsVerified = false,
                UserTypeId = UserTypes.Mobile
            };

            await _userService.AddCustomerAsync(user);

            return Ok(
                new
                {
                    user.UserGuid
                });
        }


        [HttpPost]
        [Route("ActivateMobileAccount")]
        [AllowAnonymous]
        [ApiExplorerSettings(GroupName = "Mobile")]
        public async Task<IActionResult> ActivateCustomerAccount(ActivateAccountModel activateAccountModel)
        {
            User curretnUser = (await _userService.FindAsync(u => u.UserGuid == activateAccountModel.UserGuid)).SingleOrDefault();

            if (curretnUser == null)
                return NotFound(Messages.UserNotFound);

            if (curretnUser.VerificationCode != activateAccountModel.Code || curretnUser.VerificationCodeExpiration.Value < DateTime.UtcNow)
                return NotFound(Messages.YourCodeNotMatch);

            curretnUser.IsActive = true;
            curretnUser.IsVerified = true;
            curretnUser.VerificationCode = null;
            curretnUser.VerificationCodeExpiration = null;

            await _userService.EditAsync(curretnUser);
            return Ok();
        }

        [HttpPost]
        [Route("ForgotPassword")]
        [AllowAnonymous]
        [ApiExplorerSettings(GroupName = "Mobile")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            User curretnUser = (await _userService.FindAsync(u => u.Email.Equals(forgotPasswordModel.Email, StringComparison.InvariantCultureIgnoreCase))).SingleOrDefault();

            if (curretnUser == null || !curretnUser.IsActive)
                return NotFound(Messages.UserNotFound);

            await _userService.SetUserForgotPassword(curretnUser, out string code, out DateTime expireDate);
            try
            {
                await _emailSender.SendMail(new string[] { curretnUser.Email }, "استعادة كلمة المرور", $"'رمز استعادة كلمة المرور' {code}");
            }
            catch
            {

            }
            return Ok();
        }



        [HttpPost]
        [Route("ResetPassword")]
        [AllowAnonymous]
        [ApiExplorerSettings(GroupName = "Mobile")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            User curretnUser = (await _userService.FindAsync(u => u.Email.Equals(resetPasswordModel.Email, StringComparison.InvariantCultureIgnoreCase))).SingleOrDefault();

            if (curretnUser == null || !curretnUser.IsActive)
                return NotFound(Messages.UserNotFound);

            if (string.IsNullOrWhiteSpace(curretnUser.ForgotPasswordCode) ||
                !curretnUser.ForgotPasswordExpiration.HasValue ||
                curretnUser.ForgotPasswordExpiration.Value < DateTime.UtcNow ||
                curretnUser.ForgotPasswordCode != resetPasswordModel.Code)
                return BadRequest(Messages.InvalidresetPasswordCode);

            _encryptionProvider.CreatePasswordHash(resetPasswordModel.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);

            curretnUser.ForgotPasswordCode = null;
            curretnUser.ForgotPasswordExpiration = null;

            curretnUser.Password = passwordHash;
            curretnUser.PasswordSalt = passwordSalt;

            await _userService.EditAsync(curretnUser);
            return Ok();
        }


        [HttpPost]
        [Route("CheckOnEmailAndResetCode")]
        [AllowAnonymous]
        [ApiExplorerSettings(GroupName = "Mobile")]
        public async Task<IActionResult> CheckOnEmailAndResetCode(CheckOnEmailAndResetCodeModel checkOnEmailAndResetCodeModel)
        {
            User curretnUser = (await _userService.FindAsync(u => u.Email.Equals(checkOnEmailAndResetCodeModel.Email, StringComparison.InvariantCultureIgnoreCase))).SingleOrDefault();

            if (curretnUser == null || !curretnUser.IsActive)
                return NotFound(Messages.UserNotFound);

            if (curretnUser.ForgotPasswordCode != checkOnEmailAndResetCodeModel.Code)
                return BadRequest(Messages.InvalidresetPasswordCode);

            return Ok();
        }


        [HttpPost]
        [Route("GetUserByEmail/{email}")]
        [AllowAnonymous]
        [ApiExplorerSettings(GroupName = "Mobile")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            User curretnUser = (await _userService.FindAsync(u => u.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase))).SingleOrDefault();

            if (curretnUser == null)
                return NotFound(Messages.UserNotFound);

            return Ok(curretnUser);
        }

        [HttpPut]
        [Route("EditUserMissingData")]
        [Authorize(Roles = SystemKeys.MobileRole)]
        [ApiExplorerSettings(GroupName = "Mobile")]
        public async Task<IActionResult> EditUserMissingData(UserMissingDataModel userMissingDataModel)
        {
            User curretnUser = (await _userService.FindAsync(u => u.UserGuid == userMissingDataModel.UserGuid)).SingleOrDefault();

            if (curretnUser == null || !curretnUser.IsActive)
                return NotFound(Messages.UserNotFound);

            curretnUser.Email = userMissingDataModel.Email;
            curretnUser.UserName = userMissingDataModel.UserName;
            curretnUser.PhoneNumber = userMissingDataModel.PhoneNumber;

            await _userService.EditAsync(curretnUser);

            return Ok();
        }

        [HttpPut]
        [Route("Customer/ChangePassword")]
      //  [Authorize(Roles = SystemKeys.MobileRole)]
        [ApiExplorerSettings(GroupName = "Mobile")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            User user = await _userService.GetByIdAsync(UserId);
            if (model.NewPasswordConfirmation != model.NewPassword)
                return BadRequest(Messages.NewPasswordError);

            if(!_encryptionProvider.VerifyPasswordHash(model.OldPassword, user.Password, user.PasswordSalt))
                return BadRequest(Messages.OldPasswordError);

            _encryptionProvider.CreatePasswordHash(model.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);

            user.Password = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.LastUpdateDate = DateTime.UtcNow;
            user.UpdatedBy = UserId;
            user.IsFirstLogin = false;
            await _userService.EditAsync(user);

            return Ok();
        }




    


        //[HttpPost("Customer/googleLogin")]
        //[ApiExplorerSettings(GroupName = "Mobile")]
       
        //public async Task<JsonResult> GoogleLogin(GoogleLoginRequest request)
        //{
        //    Payload payload = new Payload() ;
        //    try
        //    {
        //        payload = await ValidateAsync(request.IdToken, new ValidationSettings
        //        {
        //            Audience = new[] { "55197439828-4su7kbln47lo5nerl7id21g6i8864khc.apps.googleusercontent.com" }
        //        });
        //        // It is important to add your ClientId as an audience in order to make sure
        //        // that the token is for your application!
        //    }
        //    catch
        //    {
        //        // Invalid token
        //    }

        //    var user = await GetOrCreateExternalLoginUser("google", payload.Subject, payload.Email, payload.GivenName, payload.FamilyName);
        //    string token = _tokenProvider.GenerateTokenIdentity(user.Id.ToString(), user.Email, user.OrganizationId.ToString(), ((int)user.UserTypeId).ToString(), DateTime.Now.AddDays(300));

        //   // var token = await GenerateToken(user);
        //    return new JsonResult(token);
        //}
        //[ApiExplorerSettings(IgnoreApi = true)]
        //public async Task<User> GetOrCreateExternalLoginUser(string provider, string key, string email, string firstName, string lastName)
        //{
        //    // Login already linked to a user


        //    //var user = await _userManager.FindByLoginAsync(provider, key);
        //    //if (user != null)
        //    //    return user;


        //   var user= (await _userService.FindAsync(r => r.Email == email)).FirstOrDefault();
        //   // user = await _userManager.FindByEmailAsync(email);
        //    if (user == null)
        //    {
        //        // No user exists with this email address, we create a new one
        //        user = new User
        //        {
        //            Email = email,
        //            UserName = email,
        //            FirstName = firstName,
        //            LastName = lastName,
        //            UserTypeId=UserTypes.Mobile,
        //            UserGuid = Guid.NewGuid(),
        //            LastLoginUtcDate = DateTime.UtcNow,
        //            IsVerified = true,
        //            IsActive=true

        //        };

        //        await _userService.AddCustomerAsync(user);
        //       // await _userManager.CreateAsync(user);
        //    }

        //    // Link the user to this login
        //    //var info = new UserLoginInfo(provider, key, provider.ToUpperInvariant());
        //    //var result = await _userManager.AddLoginAsync(user, info);
        //    //if (result.Succeeded)
        //    return user;
        //}
        #endregion

        #region login




        [HttpPost("SocialMediaLogin")]
        [ApiExplorerSettings(GroupName = "Mobile")]

        public async Task<IActionResult> SocialMediaLogin(ExterLoginRequest request)
        {


            User authUser = (await _userService.FindAsync(user =>
               ((user.LoginProviders.Any(p => p.ProviderType == (int)request.ProviderType && p.Providertoken == request.ProviderToken))

           ))).FirstOrDefault();

            if (authUser == null)
                return NotFound(Messages.UserNotFound);

            string token = _tokenProvider.GenerateTokenIdentity(authUser.Id.ToString(), authUser.Email, authUser.OrganizationId.ToString(), ((int)authUser.UserTypeId).ToString(), DateTime.Now.AddDays(300));

            authUser.LastLoginUtcDate = DateTime.UtcNow;
            await _userService.EditAsync(authUser);

            return Ok(new
            {
                Token = token,
                authUser.Email,
                authUser.UserName,
                ExpireOn = DateTime.Now.AddDays(300),
                Photo = authUser.PhotoPath,
                userGuid = authUser.UserGuid.ToString(),
                branchId = authUser.BranchId,
                authUser.Id
            });
        }





        [HttpPost]
        [Route("ExternalMobileRegister")]
        [AllowAnonymous]
        [ApiExplorerSettings(GroupName = "Mobile")]
        public async Task<IActionResult> SocialMediaRegister([FromForm]ExternalRegistrationModel model)
        {
            if (await _userService.AnyAsync(u =>
                (u.PhoneNumber == model.PhoneNumber || u.Email == model.Email)))
                return BadRequest(Messages.Exists_EmailOrPhone);

            if (string.IsNullOrEmpty(model.Email)) { return BadRequest(Messages.EmailRequired); }
            if (string.IsNullOrEmpty(model.PhoneNumber)) { return BadRequest(Messages.PhoneNumberRequired); }

            User user = new User
            {
                Email = model.Email,
                IsActive = true,
                PhoneNumber = model.PhoneNumber,
                UserGuid = Guid.NewGuid(),
                UserName = model.Name,

                LastLoginUtcDate = DateTime.UtcNow,
                IsVerified = true,
                UserTypeId = UserTypes.Mobile
               ,LoginProviders = new List<LoginProviders>() { new LoginProviders() { Providertoken = model.ProviderToken, ProviderType = (int)model.ProviderType } }
            };
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
            else if (!string.IsNullOrEmpty(model.ImageUrl))
            {
                user.PhotoPath = model.ImageUrl;
            }
            await _userService.AddCustomerAsync(user);
            string token = _tokenProvider.GenerateTokenIdentity(user.Id.ToString(), user.Email, user.OrganizationId.ToString(), ((int)user.UserTypeId).ToString(), DateTime.Now.AddDays(300));

           

            return Ok(new
            {
                Token = token,
                user.Email,
                user.UserName,
                ExpireOn = DateTime.Now.AddDays(300),
                Photo = user.PhotoPath,
                userGuid = user.UserGuid.ToString(),
                branchId = user.BranchId,
                user.Id,
               
            });
         
        }





        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
       // [ApiExplorerSettings(GroupName = "SuperAdmin")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            User authUser = (await _userService.FindAsync(user =>
                ((user.Email.Equals(loginModel.EmailOrPhoneNumber, StringComparison.OrdinalIgnoreCase)) ||
                (user.PhoneNumber.Equals(loginModel.EmailOrPhoneNumber, StringComparison.OrdinalIgnoreCase)))
            )).SingleOrDefault();

            if (authUser == null || !authUser.IsActive || !_encryptionProvider.VerifyPasswordHash(loginModel.Password, authUser.Password, authUser.PasswordSalt))
                return BadRequest(Messages.Login_Invalid);


            string token = _tokenProvider.GenerateTokenIdentity(authUser.Id.ToString(), authUser.Email,authUser.OrganizationId.ToString(),((int)authUser.UserTypeId).ToString(), DateTime.Now.AddDays(300));

            authUser.LastLoginUtcDate = DateTime.UtcNow;
            await _userService.EditAsync(authUser);

            List<string> userPermissions = new List<string>();

            userPermissions = null;// (await _userPagePermissionService.FindAsync(upp => upp.UserId == authUser.Id, "SystemPagePermission")).Select(p => p.SystemPagePermission.PermissionKey).ToList();



            return Ok(new
            {
                Token = token,
                authUser.Email,
                authUser.UserName,
                ExpireOn = DateTime.Now.AddDays(300),
                Permission = userPermissions,
                Photo = authUser.PhotoPath,
                userGuid=authUser.UserGuid.ToString(),
                branchId=authUser.BranchId,
                authUser.Id,
                IsFirstLogin = authUser.IsFirstLogin
            });
        }

        ////LoginGmail

        //   clientID=55197439828-4su7kbln47lo5nerl7id21g6i8864khc.apps.googleusercontent.com
        //Client Secrit =gMpJUgKt9hjcpY1kLaqpo5Bn
        // in htmlMobilw
        // window.href=

        #endregion
    }

    public class ExterLoginRequest
    {
        public ProviderTypes ProviderType { get; set; }

        public string ProviderToken { get; set; }
      //  public string IdToken { get; set; }
    }

 
}
