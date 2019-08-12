using InterestCalculator.Api.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SamirBanjanovic.Clearent.CodeChallange.Tests
{
    [TestClass]
    public class InterestCalculatorApiTests
    {
        /*
            Visa gets 10%
            MC gets 5% interest
            Discover gets 1% interest
        */
        private static readonly IInterestCalculator _interestCalculator = new SimpleInterestCalculator();

        [TestMethod]
        public void TestSimpleInterestCalc()
        {
            var interest = _interestCalculator.ComputeInterest(100, 0.10m);

            Assert.AreEqual(10, interest);
        } 
    }
}