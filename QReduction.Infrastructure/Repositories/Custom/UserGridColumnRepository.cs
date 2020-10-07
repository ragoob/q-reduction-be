using Microsoft.EntityFrameworkCore;
using QReduction.Core.Domain.Settings;
using QReduction.Core.Repository.Custom;
using QReduction.Infrastructure.DbContexts;
using QReduction.Infrastructure.Repositories.Generic;

namespace QReduction.Infrastructure.Repositories.Custom
{
    public class UserGridColumnRepository : Repository<UserGridColumn>, IUserGridColumnRepository
    {
        private readonly DbSet<UserGridColumn> _userGridColumnsSet;
        public UserGridColumnRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _userGridColumnsSet = databaseContext.Set<UserGridColumn>();
        }
    }
}