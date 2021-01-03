using System;
using System.Collections.Generic;
using System.Text;

namespace QReduction.Core.Domain
{
    public class ShiftUserView
    {
        public int Id { get; set; }

        public int ShiftId { get; set; }

        public string WindowNumber { get; set; }

        public int  ServiceId  { get; set; }

        public TimeSpan Start { get; set; }

        public TimeSpan End { get; set; }

    }
}
