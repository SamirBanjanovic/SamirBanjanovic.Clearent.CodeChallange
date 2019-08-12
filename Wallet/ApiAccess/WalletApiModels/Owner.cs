using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wallet.ApiAccess.WalletApiModels
{
    public class Owner
    {
        public string Name { get; set; }

        public IEnumerable<CardWallet> Wallets { get; set; }
    }
}
