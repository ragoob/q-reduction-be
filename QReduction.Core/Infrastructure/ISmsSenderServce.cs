using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QReduction.Core.Infrastructure
{
    public interface ISmsSenderServce
    {
        Task<string> SendSms(string message, params string[] mobileNumbers);
    }
}
