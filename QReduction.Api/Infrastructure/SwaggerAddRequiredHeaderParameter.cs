//using Swashbuckle.AspNetCore.Swagger;
//using Swashbuckle.AspNetCore.SwaggerGen;
//using System;
//using System.Collections.Generic;

//namespace QReduction.Apis.Infrastructure
//{
//    public class SwaggerAddRequiredHeaderParameter : IOperationFilter
//    {
//        public void Apply(Swashbuckle.AspNetCore.Swagger.Operation operation, OperationFilterContext context)
//        {
//            var param = new SwaggerParams();
//            param.Name = "Authorization";
//            param.In = "header";
//            param.Description = "JWT Token";
//            param.Required = false;
//            param.Type = "string";
//            if (operation.Parameters == null)
//                operation.Parameters = new List<IParameter>();
//            if (context.MethodInfo.Name != "AuthenticateAsync")
//                operation.Parameters.Add(param);
//        }
//    }
//}
