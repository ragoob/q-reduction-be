using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using QReduction.Core.Domain.Acl;
using QReduction.Core.Service.Generic;

namespace QReduction.Core.Service.Custom
{
    public interface IRoleService : IService<Role>
    {
        void AddWithPermissions(Role Role, List<RolePagePermission> rolePermissions);
        Task AddWithPermissionsAsync(Role Role, List<RolePagePermission> rolePermissions);

        void UpdateWithPermissions(Role Role, List<RolePagePermission> rolePermissions);
        Task UpdateWithPermissionsAsync(Role Role, List<RolePagePermission> rolePermissions);
    }
}
