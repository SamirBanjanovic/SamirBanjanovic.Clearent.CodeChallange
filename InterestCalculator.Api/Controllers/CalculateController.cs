using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterestCalculator.Api.Classes;
using InterestCalculator.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace InterestCalculator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculateController : ControllerBase
    {
        //TODO: Inject calculator to compute interest `_calc.ComputeInterest(balance, cardType) => (%_intrest, $_intrest)` 
        //TODO: lookup interest rate based on `CardType`

        private readonly IInterestCalculator _interestCalculator;

        public CalculateController(IInterestCalculator interestCalculator)
        {
            _interestCalculator = interestCalculator;
        }
        
        
        [HttpGet]
        public async Task<IActionResult> Get(CardBalance cardBalance)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
           
            if(cardBalance.Balance < 0)
            {
                return null;
            }
            
            return Ok(new BalanceInterest());
        }

    }
}
