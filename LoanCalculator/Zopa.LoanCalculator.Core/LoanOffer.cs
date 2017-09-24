using System;
using System.Text;

namespace Zopa.LoanCalculator.Core
{
    public class LoanOffer
    {
        public string Currency { get; }
        public int LoanTerm { get; }
        public double LoanAmount { get; }
        public double RateInPercent { get; }
        public double MonthlyRepayment { get; }
        public double TotalRepayment { get; }

        private static bool _hasError;
        public static string ErrorMessage { get; private set; }

        public LoanOffer(double loanAmount, double rateInPercent, double monthlyRepayment, int loanTerm, string currency = "£")
        {
            LoanAmount = loanAmount;
            RateInPercent = rateInPercent;
            MonthlyRepayment = monthlyRepayment;
            TotalRepayment = loanTerm * MonthlyRepayment;
            LoanTerm = loanTerm;
            Currency = currency;
        }

        private bool IsEmpty()
        {
            var isEmpty = RateInPercent == 0 && MonthlyRepayment == 0;
            if (isEmpty)
                ErrorMessage = "It is not possible to provide a quote at this time!";
            return isEmpty;
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
            sb.AppendFormat("Requested amount: {0}{1}", Currency, LoanAmount);
            sb.AppendLine();
            sb.AppendFormat("Rate: {0:N1}%", RateInPercent * 100);
            sb.AppendLine();                      
            sb.AppendFormat("Monthly repayment: {0}{1:N2}", Currency, MonthlyRepayment);
            sb.AppendLine();            
            sb.AppendFormat("Total repayment: {0}{1:N2}", Currency,TotalRepayment);

            return sb.ToString();
        }

        public static LoanOffer SetError(string _errorMessage)
        {
            _hasError = true;
            ErrorMessage = _errorMessage;
            return new LoanOffer(0, 0, 0,0);
        }
    }
}