using System;
using System.Collections;
using System.Collections.Generic;

namespace QReduction.Core.Domain
{
    public class Branch
    {
        public int Id { get; set; }
        public int Code { get; set; }


        public int? OrganizationId { get; set; }
        public string QrCode { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Phone { get; set; }
        public string BranchAddress { get; set; }
        public string Note { get; set; }
        public int? CreateBy { get; set; }
        public DateTime CreateAt { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Organization Organization { get; set; }
        public virtual IEnumerable<BranchService> BranchServices { get; set; }
    }
}
