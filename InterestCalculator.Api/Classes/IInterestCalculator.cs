namespace InterestCalculator.Api.Classes
{
    public interface IInterestCalculator
    {
        decimal ComputeInterest(decimal balance, decimal interestRate);
    }
}