using System;
using System.Collections.Generic;
using System.Text;

namespace QReduction.Core.Domain.Acl
{
    public class PagePermission
    {
        public int Id { get; set; }
        public int PermissionTermId { get; set; }
        public int PageId { get; set; }

        public string ACLName { get; set; }

        public virtual PermissionsTerm PermissionsTerm { get; set; }
        public virtual Page Page { get; set; }

    }
}
