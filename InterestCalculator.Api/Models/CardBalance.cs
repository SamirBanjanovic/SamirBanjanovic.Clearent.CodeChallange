using System;
using System.ComponentModel.DataAnnotations;

namespace InterestCalculator.Api.Models
{
    public class CardBalance
    {
        [Required]
        public int Balance { get; set; }

        [Required]
        public string CardType { get; set; }
    }
}
