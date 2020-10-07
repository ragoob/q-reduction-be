using QReduction.Core.Domain.Acl;
using QReduction.Core.Repository.Custom;
using QReduction.Core.Repository.Generic;
using QReduction.Core.Service.Custom;
using QReduction.Infrastructure.UnitOfWorks;
using QReduction.Services.Generic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QReduction.Services.Custom
{
    public class UsersService : Service<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepository<UserRole> _userRoleRepository;

        public UsersService(IQReductionUnitOfWork unitOfWork, IUserRepository userRepository,
            IRepository<UserRole> userRoleRepository)
                : base(unitOfWork, userRepository)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;

        }

        public void AddWithDetails(User model, List<UserRole> userRole)
        {
            _userRepository.Add(model);
            if (userRole != null)
            {
                userRole.ForEach(c => c.UserId = model.Id);
                _userRoleRepository.AddRange(userRole);
            }
            _unitOfWork.SaveChanges();

        }

        public Task AddWithDetailsAsync(User model, List<UserRole> userRole)
        {
            _userRepository.Add(model);
            if (userRole != null)
            {
                userRole.ForEach(c => c.UserId = model.Id);
                _userRoleRepository.AddRange(userRole);
            }
            return _unitOfWork.SaveChangesAsync();
        }

        public void UpdateWithDetails(User model, List<UserRole> userRole)
        {
            _userRepository.Edit(model);
            if (userRole != null)
            {
                IEnumerable<UserRole> userRoleList = _userRoleRepository.Find(c => c.UserId == model.Id);
                _userRoleRepository.RemoveRange(userRoleList);

                userRole.ForEach(c => c.UserId = model.Id);
                _userRoleRepository.AddRange(userRole);
            }
            _unitOfWork.SaveChanges();
        }

        public Task UpdateWithDetailsAsync(User model, List<UserRole> userRole)
        {
            _userRepository.Edit(model);
            if (userRole != null)
            {
                IEnumerable<UserRole> userRoleList = _userRoleRepository.Find(c => c.UserId == model.Id);

                _userRoleRepository.RemoveRange(userRoleList);

                userRole.ForEach(c => c.UserId = model.Id);
                _userRoleRepository.AddRange(userRole);
            }
            return _unitOfWork.SaveChangesAsync();
        }

        public void AddCustomer(User model)
        {
            DateTime expiration = DateTime.UtcNow.AddHours(1);

            model.VerificationCode = GetRandomeCode(4);
            model.VerificationCodeExpiration = expiration;

            _userRepository.Add(model);
            _unitOfWork.SaveChanges();
        }

        public Task SetUserForgotPassword(User user, out string code, out DateTime expiration)
        {
            code = GetRandomeCode(6);
            expiration = DateTime.UtcNow.AddHours(1);

            user.ForgotPasswordCode = code;
            user.ForgotPasswordExpiration = expiration;

            _userRepository.Edit(user);
            return _unitOfWork.SaveChangesAsync();
        }

        private string GetRandomeCode(int length)
        {
            string code = "";
            Random random = new Random();

            for (int i = 0; i < length; i++)
                code += random.Next(1, 9).ToString();

            return code;
        }

        public Task AddCustomerAsync(User model)
        {
            DateTime expiration = DateTime.UtcNow.AddHours(1);
            
            model.VerificationCode = GetRandomeCode(4);
            model.VerificationCodeExpiration = expiration;

            _userRepository.Add(model);

            return _unitOfWork.SaveChangesAsync();
        }
    }
}
