using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QReduction.Core.Domain.Acl;

namespace QReduction.Apis.Models
{
    public class RoleModel
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public bool ReadOnly { get; set; }        
        public int Code { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateAt { get; set; }
        public bool IsDeleted { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public List<ApplicationPagesViewModel> ApplicationPages { get; set; }

    }
}
