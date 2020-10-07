using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QReduction.Apis.Models
{
    public class GridColumnVM
    {
        public int Id { get; set; }
        public int GridColumnId{ get; set; }
        public string ColumnName { get; set; }
        public bool IsVisible { get; set; }
    }
}
