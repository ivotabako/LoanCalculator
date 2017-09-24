namespace Zopa.LoanCalculator.Core
{
    public interface ICalculator
    {
        double GetAPYMonthlyRepayment(double requestedAmount, double interestRateInPercent, int loanTerm, int numberOfPaymentsPerYear);
    }
}