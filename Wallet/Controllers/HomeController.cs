using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Wallet.ApiAccess;
using Wallet.ApiAccess.WalletApiModels;
using Wallet.Models;

namespace Wallet.Controllers
{
    public class HomeController : Controller
    {
        private WalletApi _walletApi;
        private InterestCalculatorApi _interestApi;

        public HomeController(WalletApi walletApi,InterestCalculatorApi interestApi, IMemoryCache memoryCache)
        {
            _walletApi = walletApi;
            _interestApi = interestApi;            

        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Owner> wallets = await _walletApi.GetAll();

            return View(wallets);
        }

        // for this we'll do a post-get-redirect to avoid form reloading on client
        // this will involve use showing all wallets, when they click view interest in $
        // will post to this method, which will redirect to another action that's responsible for
        // showing all the data
        public IActionResult ShowInterestAdHoc(string ownerName, string walletName)
        {

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
    }
}
