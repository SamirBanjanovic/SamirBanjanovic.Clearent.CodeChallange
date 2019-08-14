using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wallet.ApiAccess.WalletApiModels
{
    public class Card
    {
        public decimal Balance { get; set; }
        public decimal Limit { get; set; }

        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime Expiree { get; set; }


        public decimal Interest { get; set; }
        public decimal InterestRate { get; set; }
    }
}
