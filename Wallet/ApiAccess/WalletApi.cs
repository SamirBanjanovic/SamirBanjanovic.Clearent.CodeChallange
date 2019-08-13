﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl.Http;
using Flurl;
using Wallet.ApiAccess.WalletApiModels;
using Microsoft.Extensions.Caching.Memory;

namespace Wallet.ApiAccess
{
    public class WalletApi
    {
        private readonly string _apiUri;
        private readonly IMemoryCache _memoryCache;

        public WalletApi(string apiUri, IMemoryCache memoryCache)
        {
            _apiUri = apiUri;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<Owner>> GetAll()
        {
            if (!_memoryCache.TryGetValue(CacheKeys.OwnerWalletsKey, out IEnumerable<Owner> ownerWallets))
            {
                ownerWallets = await _apiUri.GetJsonAsync<IEnumerable<Owner>>();

                _memoryCache.Set(CacheKeys.OwnerWalletsKey, ownerWallets);
            }

            return ownerWallets;
        }

        // skip using API since we'll hit the
        // cache for better performance
        public async Task<Owner> Get(string ownerName)
            => (await GetAll())
                    .FirstOrDefault(owner => string.Compare(owner.Name, ownerName, true) == 0);


    }
}
