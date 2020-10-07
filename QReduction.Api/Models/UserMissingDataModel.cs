using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QReduction.Apis.Models
{
    public class UserMissingDataModel
    {
        public Guid UserGuid { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

    }
}
