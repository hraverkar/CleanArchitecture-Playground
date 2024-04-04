using Azure.Security.KeyVault.Secrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.KeyVault
{
    public class KeyVaultManager : IKeyVaultManager
    {
        private readonly SecretClient _secretClient;
        public KeyVaultManager(SecretClient secretClient)
        {
            _secretClient = secretClient;
        }
        public async Task<string> GetSecret(string secretName)
        {
            try
            {
                KeyVaultSecret keyVaultSecret = await _secretClient.GetSecretAsync(secretName);
                return keyVaultSecret.Value;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
