using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.KeyVault
{
    public interface IKeyVaultManager
    {
        public Task<string> GetSecret(string secretName);
    }
}
