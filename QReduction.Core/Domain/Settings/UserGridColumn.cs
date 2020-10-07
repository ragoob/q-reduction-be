using System;
using System.Collections.Generic;
using System.Text;

namespace QReduction.Core.Domain.Settings
{
    public class UserGridColumn
    {
        public int Id { get; set; }
        public bool IsVisible { get; set; }
        public int UserId { get; set; }
        public int SystemGridColumnId { get; set; }
        public virtual SystemGridColumn SystemGridColumn { get; set; }

    }
}
