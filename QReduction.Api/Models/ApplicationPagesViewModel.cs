using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QReduction.Apis.Models
{
    public class ApplicationPagesViewModel
    {
        public int AppId { get; set; }
        public string AppNameAr { get; set; }
        public string AppNameEn { get; set; }
        public List<SystemPageVM> SystemPages { get; set; }

    }
}
