using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterestCalculator.Api.Classes;
using InterestCalculator.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InterestCalculator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculateController : ControllerBase
    {                
        private readonly IInterestCalculator _interestCalculator;

        public CalculateController(IInterestCalculator interestCalculator)
        {
            _interestCalculator = interestCalculator;
        }
        
        
        // api/calculate/{balance}/{interestRate}
        [HttpGet("{balance}/{interestRate}")]
        public IActionResult Get([BindRequired]decimal balance, [BindRequired]decimal interestRate)
        {            
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

                        
            return Ok(GenerateBalanceInterest(balance, interestRate));
        }

        private BalanceInterest GenerateBalanceInterest(decimal balance, decimal interestRate)
            => new BalanceInterest
            {
                Balance = balance,
                InterestPercent = interestRate,
                Interest = balance < 0 ? 0 : _interestCalculator.ComputeInterest(balance, interestRate) // computes interest base don calc injected
            };
        

    }
}
