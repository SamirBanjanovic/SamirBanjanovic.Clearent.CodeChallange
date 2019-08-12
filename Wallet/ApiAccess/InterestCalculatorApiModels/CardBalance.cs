using System;
using System.ComponentModel.DataAnnotations;

namespace Wallet.ApiAccess.InterestCalculatorApiModels
{
    public class CardBalance
    {
        [Required]
        public decimal Balance { get; set; }

        [Required]
        public decimal InterestRate { get; set; }
    }
}
