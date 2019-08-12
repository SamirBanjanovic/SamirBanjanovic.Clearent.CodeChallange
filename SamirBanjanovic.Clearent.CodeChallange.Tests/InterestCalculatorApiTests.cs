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

        [TestMethod]
        public void TestSimpleInterestCalc()
        {
            IInterestCalculator interestCalc = new SimpleInterestCalculator();


        } 
    }
}