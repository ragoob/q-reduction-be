using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using QReduction.Core.Domain;
using QReduction.Core.Domain.Settings;
using QReduction.Core.Repository.Custom;
using QReduction.Core.Service.Custom;
using QReduction.Infrastructure.UnitOfWorks;
using QReduction.Services.Generic;

namespace QReduction.Services.Custom
{
   public class SystemSettingService : Service<SystemSetting>, ISystemSettingService
    {

        private readonly ISystemSettingRepostory _systemSettingRepository;


        public SystemSettingService(IQReductionUnitOfWork unitOfWork,
          ISystemSettingRepostory systemSettingRepository) : base(unitOfWork, systemSettingRepository)
        {
            _systemSettingRepository = systemSettingRepository;
        }

        public void Add(List<SystemSetting> systemSettings)
        {
            IEnumerable<SystemSetting> systemSettingList = _systemSettingRepository.GetAll();
            _systemSettingRepository.RemoveRange(systemSettingList);
            _systemSettingRepository.AddRange(systemSettings);
            _unitOfWork.SaveChanges();

        }

        public Task AddAsync(List<SystemSetting> systemSettings)
        {
            IEnumerable<SystemSetting> systemSettingList = _systemSettingRepository.GetAll();
            _systemSettingRepository.RemoveRange(systemSettingList);
            _systemSettingRepository.AddRange(systemSettings);
            return _unitOfWork.SaveChangesAsync();
        }
    }
}
