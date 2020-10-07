using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QReduction.Apis.Infrastructure
{
    public interface IEmailSender
    {
        Task SendMail(string[] to, string subject, string body);
    }
}
