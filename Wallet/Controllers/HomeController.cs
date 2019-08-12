using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wallet.ApiAccess;
using Wallet.ApiAccess.WalletApiModels;
using Wallet.Models;

namespace Wallet.Controllers
{
    public class HomeController : Controller
    {
        private WalletApi _walletApi;
        private InterestCalculatorApi _interestApi;

        public HomeController(WalletApi walletApi,InterestCalculatorApi interestApi)
        {
            _walletApi = walletApi;
            _interestApi = interestApi;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Owner> wallets = await _walletApi.GetAll();


            return View(wallets);
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
