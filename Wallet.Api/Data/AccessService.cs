using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Wallet.Api.Models;

namespace Wallet.Api.Data
{
    public class AccessService
        : IAccessService
    {
        private readonly IDictionary<string, Owner> _ownerWallets;


        public AccessService(IConfiguration configuration)
        {
            _ownerWallets = JsonConvert
                                .DeserializeObject<IEnumerable<Owner>>(File.ReadAllText(configuration["Application:WalletDbPath"]))
                                .ToDictionary(k => k.Name);
        }

        public Task<IEnumerable<Owner>> GetWallets()
            => Task.FromResult<IEnumerable<Owner>>(_ownerWallets.Values.ToList());

        public Task<Owner> GetWallets(string owner)
        {
            if(_ownerWallets.TryGetValue(owner, out Owner ownerWallet))
            {
                return Task.FromResult(ownerWallet);
            }

            return null;
        }
    }
}
