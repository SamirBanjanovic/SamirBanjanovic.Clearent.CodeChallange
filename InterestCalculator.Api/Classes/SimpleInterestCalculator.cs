namespace InterestCalculator.Api.Classes
{
    public class SimpleInterestCalculator
        : InterestCalculatorBase
    {
        public override decimal ComputeInterest(decimal balance, decimal interestRate)
            => balance * interestRate;
    }
}