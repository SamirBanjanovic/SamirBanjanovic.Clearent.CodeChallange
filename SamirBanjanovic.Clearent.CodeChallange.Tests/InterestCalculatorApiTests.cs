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
            var interest_10 = _interestCalculator.ComputeInterest(100, 0.10m);
            var interest_05= _interestCalculator.ComputeInterest(100, 0.05m);
            var interest_01 = _interestCalculator.ComputeInterest(100, 0.01m);

            Assert.AreEqual(10, interest_10);
            Assert.AreEqual(5, interest_05);
            Assert.AreEqual(1, interest_01);
        } 
    }
}