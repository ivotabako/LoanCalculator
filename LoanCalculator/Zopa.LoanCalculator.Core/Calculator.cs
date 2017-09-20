using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zopa.LoanCalculator.Core
{
    
    public class Calculator
    {       
        /// <summary>
        /// Annual percentage yield formula 
        /// </summary>
        public double GetAPYMonthlyRepayment(decimal requestedAmount, decimal interestRateInPercent, int loanTerm)
        {
            
            return 100 * (Math.Pow((double)(1+100*interestRateInPercent/requestedAmount), (double)365/(loanTerm*30)) );
        }
    }
}
