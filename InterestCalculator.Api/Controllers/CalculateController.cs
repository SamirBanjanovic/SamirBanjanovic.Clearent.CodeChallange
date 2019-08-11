using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterestCalculator.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace InterestCalculator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculateController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get(CardBalance cardBalance)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            if(cardBalance.Balance < 0)
            {
                return 
            }
        }

    }
}
