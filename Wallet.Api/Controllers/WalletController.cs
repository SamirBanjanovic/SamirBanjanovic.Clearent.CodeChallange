using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wallet.Api.Models;

namespace Wallet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        // GET api/values
        [HttpGet("{owner}")]
        public async Task<IActionResult> GetAllWallets(string owner)
        {
            return null;
        }

        // GET api/values/5
        [HttpGet("{owner}/{walletName}")]
        public async Task<IActionResult> GetWallet(string owner, string walletName)
        {
            return "value";
        }
    }
}
