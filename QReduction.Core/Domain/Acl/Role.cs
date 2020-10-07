using System;
using System.Collections.Generic;
using System.Text;

namespace QReduction.Core.Domain.Acl
{
    public class Role
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

    }
}
