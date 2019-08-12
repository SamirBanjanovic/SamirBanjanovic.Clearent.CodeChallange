using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallet.Api.Models;

namespace Wallet.Api.Data
{
    public interface ICardAccessService
    {
        Task<IEnumerable<Owner>> GetWallets();

        Task<Owner> GetWallets(string owner);
    }
}
