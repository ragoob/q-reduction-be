using System;
using System.Collections.Generic;
using System.Text;

namespace QReduction.Core.Domain.Settings
{
    public class SystemSetting
    {
        public int Id { get; set; }
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
    }
}
