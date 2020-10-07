using System;
using System.Collections.Generic;
using System.Text;

namespace QReduction.Core.Domain.Settings
{
    public class SystemGridColumn
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool VisibilityDefault { get; set; }
        public int SystemGridId { get; set; }
        public virtual SystemGrid SystemGrid { get; set; }
    }
}
