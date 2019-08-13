using System.Collections.Generic;
using System.IO;
using System.Linq;
using InterestCalculator.Api.Classes;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Wallet.Api.Models;

namespace SamirBanjanovic.Clearent.CodeChallange.Tests
{
    [TestClass]
    public class InterestCalculatorApiTests
    {
        // could probably use some mock api
        // but for sake of simplicity we'll do this

        private static readonly IConfiguration _configuration = new ConfigurationBuilder()
                                                                        .AddJsonFile("appsettings.json")
                                                                        .Build();

        private static readonly IDictionary<string, Owner> _ownerWallets = JsonConvert
                                .DeserializeObject<IEnumerable<Owner>>(File.ReadAllText("simpleWalletDb.json"))
                                .ToDictionary(k => k.Name);

        private static readonly IDictionary<string, decimal> _interestRates =
                            JsonConvert.DeserializeObject<IDictionary<string, decimal>>(File.ReadAllText("cardInterests.json"));

        private static readonly IInterestCalculator _interestCalculator = new SimpleInterestCalculator();
        /*
            Visa gets 10%
            MC gets 5% interest
            Discover gets 1% interest
        */


        [TestMethod]
        public void TestSimpleInterestCalc()
        {
            var interest_10 = _interestCalculator.ComputeInterest(100, 0.10m);
            var interest_05 = _interestCalculator.ComputeInterest(100, 0.05m);
            var interest_01 = _interestCalculator.ComputeInterest(100, 0.01m);

            Assert.AreEqual(10, interest_10); //Visa
            Assert.AreEqual(5, interest_05);  // MC
            Assert.AreEqual(1, interest_01);  // Discover
        }

        // TODO: Add tests verifying all bullet points in challange and returning interest rate
        /*

            1 person has 1 wallet and 3 cards (1 Visa, 1 MC 1 Discover) – Each Card has a balance of $100 –
            calculate the total interest (simple interest) for this person and per card.

            In our example this is Steve Rogers

        */
        [TestMethod]
        public void TestSteveRogersInterest()
        {
            var totalInterst = _ownerWallets["Steve Rogers"]
                                .Wallets
                                .First() //we know they only have one wallet so we'll do the leap of faith access
                                .Cards
                                .Select(card =>
                                {
                                    // assume we live in a perfect world
                                    // where everything we ask for exsits
                                    var interestRate = _interestRates[card.Type];
                                    var interest = _interestCalculator.ComputeInterest(card.Balance, interestRate);

                                    Assert.AreEqual((card.Balance * interestRate), interest);

                                    return interest;
                                })
                                .Sum(); // execute
            
            // did math in head and assume i did it right -- so here we check if computer is right
            Assert.AreEqual(16, totalInterst);

        }


        /*

            1 person has 2 wallets  Wallet 1 has a Visa and Discover , wallet 2 a MC -  each card has $100 balance -
            calculate the total interest(simple interest) for this person and interest per wallet

            This is Buckey Barns

        */
        [TestMethod]
        public void TestBuckeyBarnsInterest()
        {
            var totalInterst = _ownerWallets["Buckey Barns"]
                                    .Wallets
                                    .SelectMany(w => w.Cards) // flatten the array so we can do it all in one go                                          
                                    .Select(card =>
                                    {
                                        // assume we live in a perfect world
                                        // where everything we ask for exsits
                                        var interestRate = _interestRates[card.Type];
                                        var interest = _interestCalculator.ComputeInterest(card.Balance, interestRate);

                                        Assert.AreEqual((card.Balance * interestRate), interest);

                                        return interest;
                                    })
                                    .Sum(); // execute


        }


        /*

            2 people have 1 wallet each,  person 1 has 1 wallet , with 2 cards MC and visa person 2
            has 1 wallet – 1 visa and 1 MC -  each card has $100 balance - calculate the total
            interest(simple interest) for each person and interest per wallet

        */
        [TestMethod]
        public void TestBruceAndNatasha()
        {

        }

    }
}
