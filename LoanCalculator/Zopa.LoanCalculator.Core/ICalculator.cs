namespace Zopa.LoanCalculator.Core
{
    public interface ICalculator
    {
        double GetAPYMonthlyRepayment(decimal requestedAmount, decimal interestRateInPercent, int loanTerm);
    }
}