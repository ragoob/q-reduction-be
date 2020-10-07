using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QReduction.Apis.Infrastructure
{
    public class ValidateModelFilter : ActionFilterAttribute, IActionFilter
    {
        #region Validations
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                string Message = GetInvalidModelMessage(context.ModelState);
                context.Result = new BadRequestObjectResult(Message);
            }

            base.OnActionExecuting(context);
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                string Message = GetInvalidModelMessage(context.ModelState);
                context.Result = new BadRequestObjectResult(Message);
            }

            return base.OnActionExecutionAsync(context, next);
        }
        #endregion

        #region Private
        private string GetInvalidModelMessage(ModelStateDictionary modelstate)
        {
            string Errors = "";
            foreach (var modelState in modelstate.Values)
            {

                foreach (ModelError error in modelState.Errors)
                {
                    Errors += "\n" + error.ErrorMessage;
                }
            }

            return Errors;

        }
        #endregion
    }
}
