using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using QReduction.Core.Domain.Settings;
using QReduction.Core.Service.Generic;

namespace QReduction.Core.Service.Custom
{
   public interface ISystemSettingService: IService<SystemSetting>
    {
        void Add(List<SystemSetting> systemSettings);
        Task AddAsync(List<SystemSetting> systemSettings);
    }
}
