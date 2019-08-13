using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Caching.Memory;
using Wallet.ApiAccess;
using Wallet.ApiAccess.InterestCalculatorApiModels;
using Wallet.ApiAccess.WalletApiModels;
using Wallet.Models;

namespace Wallet.Controllers
{
    public class HomeController 
        : Controller
    {
        private readonly WalletApi _walletApi;
        private readonly InterestCalculatorApi _interestApi;
        private readonly IDictionary<string, decimal> _interestRates;

        public HomeController(WalletApi walletApi, InterestCalculatorApi interestApi, IDictionary<string, decimal> interestRates)
        {
            _walletApi = walletApi;
            _interestApi = interestApi;
            _interestRates = interestRates;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Owner> ownersWallets = await _walletApi.GetAll();
            // set interest rate and values for all cards
            await SetInterestValues(ownersWallets);

            return View(ownersWallets);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        internal async Task SetInterestValues(IEnumerable<Owner> ownersWallets)
        { 
            foreach(var ow in ownersWallets)
            {
                // make a call to compute interest per card
                var ownerCards = ownersWallets.SelectMany(x => x.Wallets).SelectMany(c => c.Cards);
                foreach (var card in ownerCards)
                {
                    if (_interestRates.TryGetValue(card.Type, out decimal interestRate))
                    {
                        // compute interest from API
                        var interestDetails = await _interestApi.GetInterest(new CardBalance() { Balance = card.Balance, InterestRate = interestRate });
                        // mutate instance based on computed interest
                        card.Interest = interestDetails.Interest;
                        card.InterestRate = interestDetails.InterestPercent;
                    }
                }
            }
        }            
    }
}
