namespace InterestCalculator.Api.Classes
{
    public abstract class InterestCalculatorBase
        : IInterestCalculator
    {

        
        public abstract decimal ComputeInterest(decimal balance, decimal interestRate);        
    }
}