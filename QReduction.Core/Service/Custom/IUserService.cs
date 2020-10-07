using QReduction.Core.Domain.Acl;
using QReduction.Core.Service.Generic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QReduction.Core.Service.Custom
{
    public interface IUserService : IService<User>
    {
        void AddWithDetails(User model,List<UserRole> userRole);
        Task AddWithDetailsAsync(User model,List<UserRole> userRole);

        void UpdateWithDetails(User model, List<UserRole> userRole);
        Task UpdateWithDetailsAsync(User model,List<UserRole> userRole);
        Task SetUserForgotPassword(User curretnUser, out string code, out DateTime expireDate);




        Task AddCustomerAsync(User model);
        
    }
}
