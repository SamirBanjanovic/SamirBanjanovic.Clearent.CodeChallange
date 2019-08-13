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

        private (string CardName, string CardType, decimal Interest, decimal ExpectedInterest) ComputeCardInterest(Card card)
        {
            // assume we live in a perfect world
            // where everything we ask for exsits
            var interestRate = _interestRates[card.Type];
            var interest = _interestCalculator.ComputeInterest(card.Balance, interestRate);
            var expectedInterest = (card.Balance * interestRate);

            return (CardName: card.Name, CardType: card.Type, Interest: interest, ExpectedInterest: expectedInterest);
        }


        /*

            1 person has 1 wallet and 3 cards (1 Visa, 1 MC 1 Discover) – Each Card has a balance of $100 –
            calculate the total interest (simple interest) for this person and per card.

            In our example this is Steve Rogers

        */
        [TestMethod]
        public void TestSteveRogersInterest()
        {
            var personInterst = _ownerWallets["Steve Rogers"]
                                    .Wallets
                                    .First() //we know they only have one wallet so we'll do the leap of faith access
                                    .Cards
                                    .Select(card =>
                                    {
                                        // assert interest per card
                                        var cardInterest = ComputeCardInterest(card);

                                        Assert.AreEqual(cardInterest.ExpectedInterest, cardInterest.Interest);

                                        return cardInterest;
                                    })
                                    .Select(x => x.Interest)
                                    .Sum();

            // check if total is correct
            Assert.AreEqual(16, personInterst);

        }


        /*

            1 person has 2 wallets  Wallet 1 has a Visa and Discover , wallet 2 a MC -  each card has $100 balance -
            calculate the total interest(simple interest) for this person and interest per wallet

            This is Buckey Barns

        */
        [TestMethod]
        public void TestBuckeyBarnsInterest()
            => TestAdHocWalletPersonInterest(ownerName: "Buckey Barns", expectedTotalPersonInterest: 16);


        /*

            2 people have 1 wallet each,  person 1 has 1 wallet , with 2 cards MC and visa person 2
            has 1 wallet – 1 visa and 1 MC -  each card has $100 balance - calculate the total
            interest(simple interest) for each person and interest per wallet

        */
        [TestMethod]
        public void TestBruceAndNatasha()
        {
            //test bruce interest
            TestAdHocWalletPersonInterest(ownerName: "Bruce Banner", expectedTotalPersonInterest: 15);

            //test natasha interest
            TestAdHocWalletPersonInterest(ownerName: "Natasha Romanova", expectedTotalPersonInterest: 15);
        }

        // rules request is repeated so extract this logic into own test method that's called by others
        private void TestAdHocWalletPersonInterest(string ownerName, decimal expectedTotalPersonInterest)
        {
            var personInterest = _ownerWallets[ownerName]
                                    .Wallets
                                    .Select(wallet =>
                                    {

                                        var cardInterests = wallet
                                                            .Cards
                                                            // we don't have to check interest per card
                                                            // skip assert
                                                            .Select(card => ComputeCardInterest(card))
                                                            .ToList();

                                        var walletInterest = (WalletName: wallet.Name, WalletInterest: cardInterests.Select(x => x.Interest).Sum(), WalletExpectedInterest: cardInterests.Select(x => x.ExpectedInterest).Sum());

                                        // assert interest per wallet
                                        Assert.AreEqual(walletInterest.WalletExpectedInterest, walletInterest.WalletInterest);

                                        return walletInterest;
                                    })
                                    .Select(x => x.WalletInterest)
                                    .Sum(); // execute

            // assert total interest
            Assert.AreEqual(expectedTotalPersonInterest, personInterest);
        }

    }
}
