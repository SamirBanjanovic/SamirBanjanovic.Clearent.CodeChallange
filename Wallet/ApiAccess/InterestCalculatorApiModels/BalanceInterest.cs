using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wallet.ApiAccess.InterestCalculatorApiModels
{
    public class BalanceInterest
    {
        public decimal Balance { get; set; }

        public decimal InterestPercent { get; set; }

        public decimal Interest { get; set; }


    }
}
