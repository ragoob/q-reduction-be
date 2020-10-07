using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QReduction.Apis.Infrastructure
{
    public interface ITokenProvider
    {
        string GenerateTokenIdentity(string id, string name,string organizationId,string userTypeId);
        string GenerateTokenIdentity(string id, string name, string organizationId, string userTypeId, DateTime Expires);

        string CustomerGenerateTokenIdentity(string id, string name);
        string CustomerGenerateTokenIdentity(string id, string name, DateTime Expires);


    }
}
