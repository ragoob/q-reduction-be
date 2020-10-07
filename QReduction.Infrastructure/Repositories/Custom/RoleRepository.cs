using Microsoft.EntityFrameworkCore;
using QReduction.Core.Domain.Acl;
using QReduction.Core.Repository.Custom;
using QReduction.Infrastructure.DbContexts;
using QReduction.Infrastructure.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QReduction.Infrastructure.Repositories.Custom
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly DbSet<Role> _rolesSet;
        private readonly DbSet<RolePagePermission> _rolePagePermissionsSet;
        public RoleRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _rolesSet = databaseContext.Set<Role>();
            _rolePagePermissionsSet = databaseContext.Set<RolePagePermission>();
        }

        public void AddWithPermissions(Role entity, List<RolePagePermission> rolePagePermissions)
        {
            _rolesSet.Add(entity);

            if (rolePagePermissions != null && rolePagePermissions.Count > 0)
            {
                rolePagePermissions.ForEach(rp => rp.RoleId = entity.Id);
                _rolePagePermissionsSet.AddRange(rolePagePermissions);
            }
        }

        public void Delete(Role Role)
        {
            if (Role == null)
                throw new NullReferenceException("Role");
            _rolesSet.Remove(Role);
        }



        public void UpdateWithPermissions(Role entity, List<RolePagePermission> rolePagePermissions)
        {
            _rolesSet.Update(entity);

            List<RolePagePermission> RolePagePermissions = _rolePagePermissionsSet.Where(e => e.RoleId == entity.Id).ToList();
            _rolePagePermissionsSet.RemoveRange(RolePagePermissions);

            if (rolePagePermissions != null && rolePagePermissions.Count > 0)
            {
                rolePagePermissions.ForEach(rp => rp.RoleId = entity.Id);
                _rolePagePermissionsSet.AddRange(rolePagePermissions);
            }
        }

       
    }
}
