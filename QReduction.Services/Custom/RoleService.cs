using QReduction.Services.Generic;
using QReduction.Core.Domain.Acl;
using QReduction.Core.Repository.Custom;
using QReduction.Core.Service.Custom;
using QReduction.Infrastructure.UnitOfWorks;
using System.Threading.Tasks;
using System.Collections.Generic;
using QReduction.Core.UnitOfWork;

namespace QReduction.Services.Custom
{

    public class RoleService : Service<Role>, IRoleService
    {
        private IRoleRepository RoleRepository
        {
            get
            {
                return (IRoleRepository)this._repository;
            }
        }

        public RoleService(
            IQReductionUnitOfWork unitOfWork, IRoleRepository roleRepository) : base(unitOfWork, roleRepository) { }

        public void AddWithPermissions(Role Role, List<RolePagePermission> rolePermissions)
        {
            RoleRepository.AddWithPermissions(Role, rolePermissions);
            _unitOfWork.SaveChanges();
        }

        public Task AddWithPermissionsAsync(Role Role, List<RolePagePermission> rolePermissions)
        {
            RoleRepository.AddWithPermissions(Role, rolePermissions);
            return _unitOfWork.SaveChangesAsync();
        }

        public void UpdateWithPermissions(Role Role, List<RolePagePermission> rolePermissions)
        {
            RoleRepository.UpdateWithPermissions(Role, rolePermissions);
            _unitOfWork.SaveChanges();
        }

        public Task UpdateWithPermissionsAsync(Role Role, List<RolePagePermission> rolePermissions)
        {
            RoleRepository.UpdateWithPermissions(Role, rolePermissions);
            return _unitOfWork.SaveChangesAsync();
        }
    }

}

