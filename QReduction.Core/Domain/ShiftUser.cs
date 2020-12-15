using QReduction.Core.Domain.Acl;
using System;

namespace QReduction.Core.Domain
{
    public class ShiftUser
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ShiftId { get; set; }
        public string WindowNumber { get; set; }

        public DateTime CreatedAt { get; set; }
        public int? ServiceId { get; set; }
        public User User { get; set; }
        public Shift Shift { get; set; }

        public Service  Service { get; set; }

    }
}
