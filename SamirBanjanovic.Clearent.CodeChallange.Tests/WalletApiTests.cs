using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wallet.Api.Data;

namespace SamirBanjanovic.Clearent.CodeChallange.Tests
{
    [TestClass]
    public class WalletApiTests
    {
        private static readonly IConfiguration _configuration = new ConfigurationBuilder()
                                                                        .AddJsonFile("appsettings.json")
                                                                        .Build();

        [TestMethod]
        public async Task DataAccesTest()
        {
            IAccessService accessService = new AccessService(_configuration);
            var ownerWallets = await accessService.GetWallets();

            foreach(var ow in ownerWallets)
            {
                Assert.IsNotNull(ow.Wallets);                
            }

            Assert.AreEqual(4, ownerWallets.Count());
        }
    }
}
