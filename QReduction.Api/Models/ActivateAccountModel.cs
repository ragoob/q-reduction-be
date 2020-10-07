using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QReduction.Apis.Models
{
    public class ActivateAccountModel
    {
        public Guid UserGuid { get; set; }

        public string Code { get; set; }

    }
}
