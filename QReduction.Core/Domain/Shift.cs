using QReduction.Core.Domain.Acl;
using System;
using System.Collections.Generic;

namespace QReduction.Core.Domain
{
    public class Shift
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }

        public DateTime StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public int BranchId { get; set; }
        public bool IsEnded { get; set; }
        public string QRCode { get; set; }
        public int? UserIdSupport { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? CreateBy { get; set; }
        public DateTime CreateAt { get; set; }
        public int? UpdateBy { get; set; }

        public Branch Branch { get; set; }
        public User UserSupport { get; set; }

        //public ICollection<ShiftUser> ShiftUsers { get; set; }
    }
}
