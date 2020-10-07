using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using QReduction.Core.Domain.Acl;
using QReduction.Core.Repository.Generic;

namespace QReduction.Core.Repository.Custom
{
    public interface IRoleRepository : IRepository<Role>
    {
        void AddWithPermissions(Role entity, List<RolePagePermission> rolePagePermissions);
        void UpdateWithPermissions(Role entity, List<RolePagePermission> rolePagePermissions);
        void Delete(Role Role);
    }
}
