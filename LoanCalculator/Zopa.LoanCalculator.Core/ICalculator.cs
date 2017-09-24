namespace Zopa.LoanCalculator.Core
{
    public interface ICalculator
    {
        double GetMonthlyPayment(double requestedAmount, double interestRateInPercent, int loanTerm, int numberOfPaymentsPerYear);
    }
}