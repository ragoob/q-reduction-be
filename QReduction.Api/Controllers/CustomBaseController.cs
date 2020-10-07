using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using QReduction.Core.Extensions;
using QReduction.Api;

namespace QReduction.Apis.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        public int UserId
        {
            get
            {
                if (User.Identity.IsAuthenticated)
                {
                    int.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value, out int userId);
                    return userId;
                }
                else return 0;
            }
        }
        public int OrganizationId
        {
            get
            {
                if (User.Identity.IsAuthenticated)
                {
                    int.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.System).Value, out int organizationId);
                    return organizationId;
                }
                else return 0;
            }
        }


        public bool IsArabic { get => !Messages.LangKey.Equals("en", StringComparison.OrdinalIgnoreCase); }

        [NonAction]
        internal protected DateTime GetDateFromString(string dateString)
        {
            return DateTime.ParseExact(dateString, "dd-MM-yyyy", CultureInfo.GetCultureInfo("en-GB"));
        }

        public T ShallowMap<T>(object fromObject, T toObject = null) where T : class, new()
        {
            if (fromObject == null)
                return null;

            T instance = toObject == null ? new T() : toObject;
            Type instanceType = typeof(T);
            Type fromType = fromObject.GetType();

            fromType.GetProperties()
                .ForEach(p =>
                {
                    PropertyInfo instanceProp = instanceType.GetProperty(p.Name);
                    if (instanceProp == null)
                        return;

                    if (instanceProp.PropertyType.FullName.StartsWith("System.") || instanceProp.PropertyType.IsEnum)
                    {
                        try
                        {
                            instanceProp.SetValue(instance, p.GetValue(fromObject));
                        }
                        catch
                        {

                        }
                    }
                });
            return instance;
        }
    }
}
