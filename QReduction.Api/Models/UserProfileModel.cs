using QReduction.Core.Domain.Acl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QReduction.Apis.Models
{
    public class UserProfileModel
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PhotoPath { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public UserTypes? UserTypeId { get; set; }
        public virtual UserType UserType { get; set; }
    }

    public class HelpSupport
    {
        public int Id { get; set; }

        public string MessageTitle { get; set; }
        public string Message { get; set; }

        public int? UserId { get; set; }
        
    }
}
