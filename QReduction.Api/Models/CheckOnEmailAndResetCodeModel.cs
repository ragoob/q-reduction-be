﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QReduction.Apis.Models
{
    
    public class CheckOnEmailAndResetCodeModel
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
