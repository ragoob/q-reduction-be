using System;
using System.Collections.Generic;
using System.Text;

namespace QReduction.Core.Domain.Acl
{
    public class UserType
    {
        public UserTypes Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
    }

    public enum UserTypes
    {
        //AdminUser = 1,
        //CustomerUser = 2,
        //MobileUser = 3,

        SuperAdmin = 1,
        OrganizationAdmin = 2,
        ShiftSupervisor = 3,
        Tailor=4,
        Mobile=5


    }
}
