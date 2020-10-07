using System;
using System.Collections.Generic;
using System.Text;

namespace QReduction.Core.Domain.Acl
{
    public class RolePagePermission
    {
        public int Id { get; set; }
        public int PagePermissionId { get; set; }
        public int RoleId { get; set; }
      
        public virtual PagePermission PagePermission { get; set; }
        public virtual Role Role { get; set; }
    }
}
