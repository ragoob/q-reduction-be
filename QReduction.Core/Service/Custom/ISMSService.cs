using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QReduction.Core.Service.Custom
{
    public interface ISMSService
    {
        Task<string> Send(string mobileNumber, string message);

    }
}
