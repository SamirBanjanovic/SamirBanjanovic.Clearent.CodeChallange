namespace InterestCalculator.Api.Classes
{
    public abstract class InterestCalculatorBase
        : IInterestCalculator
    {      
        // not the best practice to have such an empty abstract class
        // but in the future as more compute classes are generated we can 
        // start refactoring and adding shared code here
        public abstract decimal ComputeInterest(decimal balance, decimal interestRate);        
    }
}