using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using QReduction.Core.Infrastructure;
using QReduction.Core.Repository.Custom;
using QReduction.Core.Repository.Generic;
using QReduction.Core.Service.Custom;
using QReduction.Core.Service.Generic;
using QReduction.Infrastructure.Db;
using QReduction.Infrastructure.DbContexts;
using QReduction.Infrastructure.Repositories.Custom;
using QReduction.Infrastructure.Repositories.Generic;
using QReduction.Infrastructure.UnitOfWorks;
using QReduction.Services.Generic;
using QReduction.Services.Custom;


namespace QReduction.Apis.Infrastructure
{
    public static class DependancyInjectionConfig
    {
        public static IServiceProvider ServiceProvider { get; set; }

        internal static ServiceProvider Config(IServiceCollection services, IConfiguration configuration)
        {
            #region DbContext ...
            services.AddDbContext<IDatabaseContext, DatabaseContext>(option =>
            option.UseSqlServer(configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);
            services.AddTransient<IQReductionUnitOfWork, QReductionUnitOfWork>();
            #endregion

            #region Repositories ...
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IUserRepository), typeof(UserRepository));
            services.AddTransient(typeof(IRoleRepository), typeof(RoleRepository));
            services.AddTransient(typeof(IUserGridColumnRepository), typeof(UserGridColumnRepository));
            services.AddTransient(typeof(ISystemSettingRepostory), typeof(SystemSettingRepostory));
            services.AddTransient(typeof(IShiftQueueRepository), typeof(ShiftQueueRepository));


            #endregion

            #region Services ...
            services.AddTransient(typeof(IService<>), typeof(Service<>));
            services.AddTransient(typeof(IUserService), typeof(UsersService));
            services.AddTransient(typeof(IRoleService), typeof(RoleService));
            services.AddTransient(typeof(IUserGridColumnService), typeof(UserGridColumnService));
            services.AddTransient(typeof(ISystemSettingService), typeof(SystemSettingService));
            services.AddTransient(typeof(ISMSService), typeof(SMSService));

            services.AddTransient(typeof(IShiftQueueService), typeof(ShiftQueueService));

            services.AddScoped<IEncryptionProvider, EncryptionProvider>();
            services.AddScoped<ITokenProvider, TokenProvider>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IParameterFactory, ParameterFactory>();



            #endregion
            
            
            #region SingleTonePdf

            //services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            #endregion
            
            ServiceProvider = services.BuildServiceProvider();
            return (ServiceProvider as ServiceProvider);
        }
    }
}
