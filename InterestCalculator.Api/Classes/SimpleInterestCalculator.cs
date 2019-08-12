namespace InterestCalculator.Api.Classes
{
    public class SimpleInterestCalculator
        : InterestCalculatorBase
    {
        public SimpleInterestCalculator()
        {
        }

        public override decimal ComputeInterest(decimal balance, decimal interestRate)
        {
            throw new System.NotImplementedException();
        }
    }
}