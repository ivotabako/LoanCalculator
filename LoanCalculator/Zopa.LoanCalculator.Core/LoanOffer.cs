namespace Zopa.LoanCalculator.Core
{
    public class LoanOffer
    {
        public decimal RateInPercent { get; }
        public decimal MonthlyRepayment { get; }
        public decimal TotalRepayment { get; }

        public LoanOffer(decimal rateInPercent, decimal monthlyRepayment)
        {
            RateInPercent = rateInPercent;
            MonthlyRepayment = monthlyRepayment;
            TotalRepayment = 12 * MonthlyRepayment;
        }
    }
}