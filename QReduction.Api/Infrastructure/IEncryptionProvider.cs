using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QReduction.Apis.Infrastructure
{
    public interface IEncryptionProvider
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
    }
}
