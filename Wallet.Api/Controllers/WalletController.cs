using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Wallet.Api.Data;
using Wallet.Api.Models;

namespace Wallet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly ICardAccessService _accessService;

        public WalletController(ICardAccessService accessService)
        {
            _accessService = accessService;
        }

        // GET api/wallet/{owner}
        [HttpGet("{owner}")]
        public async Task<IActionResult> GetAllWallets([BindRequired] string owner)
        {
            if(ModelState.IsValid)
            {
                return Ok(await _accessService.GetWallets(owner).ConfigureAwait(false));
            }

            return BadRequest();
        }

                                       
    }
}
