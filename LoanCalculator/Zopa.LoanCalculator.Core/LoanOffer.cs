using System;
using System.Text;

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
            ErrorMessage = "It is not possible to provide a quote at that time!";
            return RateInPercent == 0 && MonthlyRepayment == 0;
        }

        public void Print(Action<string, object> write)
        {            
            if (_hasError || IsEmpty())
            {
                write(ErrorMessage, null);
            }
            
            write(GetValidOutput(), null);
        }

        private string GetValidOutput()
        {
            var sb = new StringBuilder();
            sb.Append("Requested amount: £");
            sb.AppendLine(LoanAmount.ToString());
            sb.Append("Rate: ");
            sb.Append(RateInPercent.ToString());
            sb.AppendLine("%");
            sb.Append("Monthly repayment: £");
            sb.AppendLine(MonthlyRepayment.ToString());
            sb.Append("Total repayment: £");
            sb.AppendLine(TotalRepayment.ToString());

            return sb.ToString();

        }

        internal static LoanOffer SetError(string _errorMessage)
        {
            _hasError = true;
            ErrorMessage = _errorMessage;
            return new LoanOffer(0, 0, 0,0);
        }
    }
}