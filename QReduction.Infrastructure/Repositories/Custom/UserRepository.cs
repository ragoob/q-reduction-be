using Microsoft.EntityFrameworkCore;
using QReduction.Core.Domain.Acl;
using QReduction.Core.Repository.Custom;
using QReduction.Infrastructure.DbContexts;
using QReduction.Infrastructure.Repositories.Generic;

namespace QReduction.Infrastructure.Repositories.Custom
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DbSet<User> _entitiesSet;
        public UserRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _entitiesSet = databaseContext.Set<User>();
        }
    }
}
