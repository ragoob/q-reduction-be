using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QReduction.Api.Models
{
    public class ShiftQueueVM
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

        public string currentTime { get; set; }
        public string PushId { get; set; }

    }
}
