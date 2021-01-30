using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using QReduction.Apis.Infrastructure;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.Hosting;
namespace QReduction.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            DependancyInjectionConfig.Config(services, Configuration);
            JWTConfig.Config(services, Configuration);
            services.AddHttpContextAccessor();
            services.AddControllers();
            services.AddNodeServices();
            services.AddCors(o => o.AddPolicy("QReductionPolicy", buillder =>
            {
                buillder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));

            //Google Plus
            //services.AddAuthentication().AddGoogle(googleOptions =>
            //{
            //    googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
            //    googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            //});
            //services.AddControllers().AddNewtonsoftJson(options =>
            //options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


            //SwaggerConfig(services);
            services.AddControllers().AddNewtonsoftJson(options =>
       options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
     );

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();

            //app.UseCors();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllers();
            });
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            //app.UseSwagger();
            // Enable middleware to serve swagger-ui(HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            //});
        }

        #region Private

        ////private void SwaggerConfig(IServiceCollection services)
        //{
        //    services.AddSwaggerGen(c =>
        //    {

        //        c.OperationFilter<SwaggerAddRequiredHeaderParameter>();
        //        c.SwaggerDoc("Admin", new Info { Description = "QReduction web api Documentation", Title = "QReduction Admin", Version = "Admin" });
        //        c.SwaggerDoc("Mobile", new Info { Description = "QReduction Mobile api Documentation", Title = "QReduction Mobile", Version = "Mobile" });
        //        c.SwaggerDoc("Customer", new Info { Description = "QReduction Customer api Documentation", Title = "QReduction Customer", Version = "Customer" });
        //        c.DescribeAllEnumsAsStrings();
        //        c.EnableAnnotations();
        //    });
        //}

    }
    #endregion

}

