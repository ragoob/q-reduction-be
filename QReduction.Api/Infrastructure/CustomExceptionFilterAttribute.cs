using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QReduction.Api;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
//using QReduction.Apis.Resources;

namespace QReduction.Apis.Infrastructure
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            Exception ex = context.Exception;
            context.ExceptionHandled = true;

            if (IsDeleteException(ex))
                context.Result = new BadRequestObjectResult(Messages.Delete_RelatedDelete);

            base.OnException(context);
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            Exception ex = context.Exception;
            context.ExceptionHandled = true;

            if (IsDeleteException(ex))
                context.Result = new BadRequestObjectResult(Messages.Delete_RelatedDelete);

            return base.OnExceptionAsync(context);
        }

        private bool IsDeleteException(Exception exc)
        {
            var baseException = exc.GetBaseException();
            return  (baseException as SqlException) != null && (baseException as SqlException).Number == 547 && baseException.Message.StartsWith("THE DELETE", StringComparison.OrdinalIgnoreCase);
        }
    }
}
