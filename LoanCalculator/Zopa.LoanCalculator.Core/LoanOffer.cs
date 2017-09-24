using System;
using System.Text;

namespace Zopa.LoanCalculator.Core
{
    public class LoanOffer
    {        
        public string Currency { get; }
        public int LoanTerm { get; }
        public double LoanAmount { get; }
        public double Rate { get; }
        public double MonthlyRepayment { get; }
        public double TotalRepayment { get; }

        public bool HasError { get; private set; }
        public string ErrorMessage { get; private set; }

        public LoanOffer(double loanAmount, double rate, double monthlyRepayment, int loanTerm, string currency = "£")
        {
            LoanAmount = loanAmount;
            Rate = rate;
            MonthlyRepayment = monthlyRepayment;
            TotalRepayment = loanTerm * MonthlyRepayment;
            LoanTerm = loanTerm;
            Currency = currency;
        }

        public LoanOffer(string _errorMessage)
        {
            HasError = true;
            ErrorMessage = string.Format("Error in the lenders file or the requested loan amount: \n\r {0}", _errorMessage);
        }       

        public void Print(Action<string, object> writer)
        {            
            if (HasError)
            {
                writer(ErrorMessage, null);
            }
            else
            {
                writer(GetValidOutput(), null);
            }            
        }

        private string GetValidOutput()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("Requested amount: {0}{1}", Currency, LoanAmount);
            sb.AppendLine();
            sb.AppendFormat("Rate: {0:N1}%", Rate * 100);
            sb.AppendLine();                      
            sb.AppendFormat("Monthly repayment: {0}{1:N2}", Currency, MonthlyRepayment);
            sb.AppendLine();            
            sb.AppendFormat("Total repayment: {0}{1:N2}", Currency,TotalRepayment);

            return sb.ToString();
        }        
    }
}