using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QReduction.Core.Domain.Acl;

namespace QReduction.Apis.Models
{
    public class SystemPageVM
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public IEnumerable<SystemPagePermissionVM> PagePermissions { get; set; }


    }
    public class SystemPagePermissionVM
    {
        public int Id { get; set; }
        public int PermissionTermId { get; set; }
        public string PermissionTermNameAr { get; set; }
        public string PermissionTermNameEn { get; set; }
        public bool Included { get; set; }
    }


    public class AssignServicesToBranch
    {
        public int BranchId { get; set; }

        public List<int> ServicesIds { get; set; }


    }

}
