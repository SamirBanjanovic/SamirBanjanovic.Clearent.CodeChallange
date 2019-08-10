using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wallet.Api.Models
{
    public class Wallet
    {
        public string Owner { get; set; }

        public string Name { get; set; }

        public IEnumerable<Card> Cards { get; set; }
    }
}
