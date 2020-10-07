using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QReduction.Api;

namespace QReduction.Apis.Infrastructure
{
    public class CustomAuthorizationFilterAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string _authPattern;

        public CustomAuthorizationFilterAttribute(string authPattern = null)
        {
            _authPattern = authPattern;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated || !CheckCustomAuth(context))
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = Messages.Access_Unauthorized
                };
        }

        //object obj = new object();

        private bool CheckCustomAuth(AuthorizationFilterContext context)
        {

            if (string.IsNullOrWhiteSpace(_authPattern))
                return true;
            bool hasPermission = true;
            //int.TryParse(context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value, out int userId);
            //using (var scopedProvider = DependancyInjectionConfig.ServiceProvider.CreateScope())
            //{
            //    var userService = scopedProvider.ServiceProvider.GetService<IUserService>();
            //    if (userService.GetById(userId).IsAdmin)
            //        return true;
            //}

            ////IUserService userService = DependancyInjectionConfig.ServiceProvider.GetRequiredService<IUserService>();

            ////if (userService.GetUserUsingScript(userId).IsAdmin)
            ////if (userService.GetById(userId).IsAdmin)
            ////    return true;
            //bool hasPermission = false;
            //using (var scopedProvider = DependancyInjectionConfig.ServiceProvider.CreateScope())
            //{
            //    IUserPagePermissionService userPermissionService = scopedProvider.ServiceProvider.GetRequiredService<IUserPagePermissionService>();
            //    hasPermission = userPermissionService //.UserPermissionExistFromScript(userId, _authPattern);
            //  .Any(up => up.UserId == userId && up.SystemPagePermission.PermissionKey.Equals(_authPattern));
            //}
            //IUserPagePermissionService userPermissionService = DependancyInjectionConfig.ServiceProvider.GetRequiredService<IUserPagePermissionService>();



            return hasPermission;

        }

    }
}
