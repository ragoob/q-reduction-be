using Microsoft.EntityFrameworkCore;
using QReduction.Core.Domain.Settings;
using QReduction.Core.Repository.Custom;
using QReduction.Infrastructure.DbContexts;
using QReduction.Infrastructure.Repositories.Generic;

namespace QReduction.Infrastructure.Repositories.Custom
{
    public class SystemSettingRepostory : Repository<SystemSetting>, ISystemSettingRepostory
    {
        private readonly DbSet<SystemSetting> _entitiesSet;
        public SystemSettingRepostory(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _entitiesSet = databaseContext.Set<SystemSetting>();
        }
    }
}
