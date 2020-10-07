using QReduction.Core.Domain.Acl;
using System;
using System.Collections.Generic;
using System.Text;

namespace QReduction.Core.Domain.Acl
{
    public class UserRole
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
