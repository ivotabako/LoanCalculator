namespace Zopa.LoanCalculator.Core
{
    public interface ICalculator
    {
        double GetMonthlyPayment(int requestedAmount, double interestRateInPercent, int loanTerm, int numberOfPaymentsPerYear);
    }
}