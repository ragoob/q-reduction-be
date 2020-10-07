using QReduction.Core.Domain.Acl;
using System;

namespace QReduction.Core.Domain
{
    public class ShiftQueue
    {
        public int Id { get; set; }
        public int UserTurn { get; set; }
        public int ShiftId { get; set; }
        public bool IsServiceDone { get; set; }
        public int UserIdMobile { get; set; }
        public int? UserIdBy { get; set; }
        public int ServiceId { get; set; }
        public string WindowNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public Shift Shift { get; set; }
        public User UserMobile { get; set; }
        public User UserBy { get; set; }
        public Service Service { get; set; }

        public string PushId { get; set; }



    }
}
