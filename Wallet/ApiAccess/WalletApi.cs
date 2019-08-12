using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl.Http;
using Flurl;
using Wallet.ApiAccess.WalletApiModels;

namespace Wallet.ApiAccess
{
    public class WalletApi
    {
        private readonly string _apiUri;

        public WalletApi(string apiUri)
        {
            _apiUri = apiUri;
        }

        public async Task<IEnumerable<Owner>> GetAll()
            => await _apiUri.GetJsonAsync<IEnumerable<Owner>>();

        public async Task<Owner> Get(string ownerName)
            => await _apiUri.AppendPathSegment(ownerName).GetJsonAsync<Owner>();
    }
}
