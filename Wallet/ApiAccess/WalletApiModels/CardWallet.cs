using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wallet.ApiAccess.WalletApiModels
{
    public class CardWallet
    {
        public string Name { get; set; }
        public IEnumerable<Card> Cards { get; set; }
    }
}
