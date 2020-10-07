using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QReduction.Apis.Infrastructure
{
    public class LangActionFilter : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string lang = context.HttpContext.Request.Headers["Lang"];
            SetLang(lang);

            base.OnActionExecuting(context);
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string lang = context.HttpContext.Request.Headers["Lang"];
            SetLang(lang);

            return base.OnActionExecutionAsync(context, next);
        }

        private void SetLang(string lang)
        {
            string threadCulture = (lang??"en-US").Split('-')[0].Equals("ar", StringComparison.OrdinalIgnoreCase) ? "ar-EG" : "en-US";

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(threadCulture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(threadCulture);
        }
    }
}
