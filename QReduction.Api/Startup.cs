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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddMvcOptions(o =>
                {
                    o.Filters.Add(new LangActionFilter());

                }).AddJsonOptions(
            options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        );

            DependancyInjectionConfig.Config(services, Configuration);
            services.AddNodeServices();
            JWTConfig.Config(services, Configuration);
           
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

            SwaggerConfig(services);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
            app.UseSwagger();
            app.UseCors("QReductionPolicy");
            app.UseStaticFiles();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/Admin/swagger.json", "Application web api documentation");
                c.SwaggerEndpoint("/swagger/Mobile/swagger.json", "Mobile web api documentation");
                c.SwaggerEndpoint("/swagger/Customer/swagger.json", "Customer web api documentation");
                
            });
        }

        #region Private

        private void SwaggerConfig(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
               
                c.OperationFilter<SwaggerAddRequiredHeaderParameter>();
                c.SwaggerDoc("Admin", new Info { Description = "QReduction web api Documentation", Title = "QReduction Admin", Version = "Admin" });
                c.SwaggerDoc("Mobile", new Info { Description = "QReduction Mobile api Documentation", Title = "QReduction Mobile", Version = "Mobile" });
                c.SwaggerDoc("Customer", new Info { Description = "QReduction Customer api Documentation", Title = "QReduction Customer", Version = "Customer" });
                c.DescribeAllEnumsAsStrings();
                c.EnableAnnotations();
            });
        }

        #endregion

    }
}
