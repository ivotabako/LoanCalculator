using System;

namespace Zopa.LoanCalculator.Core
{
    public class LoanOffer
    {
        public int LoanTerm { get; }
        public decimal LoanAmount { get; }
        public decimal RateInPercent { get; }
        public double MonthlyRepayment { get; }
        public double TotalRepayment { get; }

        private static bool _hasError;
        public static string ErrorMessage { get; private set; }

        public LoanOffer(decimal loanAmount, decimal rateInPercent, double monthlyRepayment, int loanTerm)
        {
            LoanAmount = loanAmount;
            RateInPercent = rateInPercent;
            MonthlyRepayment = monthlyRepayment;
            TotalRepayment = loanTerm * MonthlyRepayment;
            LoanTerm = loanTerm;
        }

        public bool IsEmpty()
        {
            return RateInPercent == 0 && MonthlyRepayment == 0;
        }

        public void Print(Action<string, object> write)
        {
            if (_hasError)
            {

            }

            if (IsEmpty())
            {

            }

            string output =
@"Requested amount: £{0}

Rate: {1} %
    
Monthly repayment: £{2}

Total repayment: £{3}
";
            write(string.Format( output, LoanAmount, RateInPercent, MonthlyRepayment, TotalRepayment), null);
        }

        internal static LoanOffer SetError(string _errorMessage)
        {
            _hasError = true;
            ErrorMessage = _errorMessage;
            return new LoanOffer(0, 0, 0,0);
        }
    }
}