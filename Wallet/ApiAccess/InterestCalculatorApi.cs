using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallet.ApiAccess.InterestCalculatorApiModels;
using Flurl;
using Flurl.Http;

namespace Wallet.ApiAccess
{
    public class InterestCalculatorApi
    {
        private readonly string _apiUri;

        public InterestCalculatorApi(string apiUri)
        {
            _apiUri = apiUri;
        }

        public async Task<BalanceInterest> GetInterest(CardBalance cardBalance)
            => await _apiUri.AppendPathSegments(cardBalance.Balance, cardBalance.InterestRate)
                            .GetJsonAsync<BalanceInterest>()
                            .ConfigureAwait(false);

        
    }
}
