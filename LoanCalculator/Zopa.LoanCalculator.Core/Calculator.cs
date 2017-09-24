using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zopa.LoanCalculator.Core
{
    
    public class Calculator : ICalculator
    {       
        /// <summary>
        /// Annual percentage yield formula 
        /// </summary>
        public double GetAPYMonthlyRepayment(double requestedAmount, double interestRateInPercent, int loanTerm, int numberOfPaymentsPerYear)
        {            
            var discountFactor = ((Math.Pow(1 + interestRateInPercent / numberOfPaymentsPerYear, loanTerm) - 1) / (interestRateInPercent / numberOfPaymentsPerYear * Math.Pow(1 + interestRateInPercent / numberOfPaymentsPerYear, loanTerm)));
            return requestedAmount / discountFactor;           
        }
    }
}
