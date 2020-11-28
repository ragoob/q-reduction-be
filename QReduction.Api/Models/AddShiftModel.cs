using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QReduction.Api.Models
{
    public class AddShiftModel
    {
        public int BranchId { get; set; }

        public List<ShiftModel> Shifts { get; set; }
    }


    public class ShiftModel
    {
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
    }
}
