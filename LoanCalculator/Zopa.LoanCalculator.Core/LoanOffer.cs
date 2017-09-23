using System;

namespace Zopa.LoanCalculator.Core
{
    public class LoanOffer
    {
        public decimal RateInPercent { get; }
        public double MonthlyRepayment { get; }
        public double TotalRepayment { get; }
        public static LoanOffer Empty { get; internal set; }
        
        static LoanOffer()
        {
            Empty = new LoanOffer(0, 0);
        }

        public LoanOffer(decimal rateInPercent, double monthlyRepayment)
        {
            RateInPercent = rateInPercent;
            MonthlyRepayment = monthlyRepayment;
            TotalRepayment = 12 * MonthlyRepayment;
        }

        public bool IsEmpty()
        {
            return RateInPercent == 0 && MonthlyRepayment == 0;
        }

        public void Print(Action<string, object> write)
        {
            
        }

        internal static LoanOffer SetError(string _errorMessage)
        {
            throw new NotImplementedException();
        }
    }
}