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
        public double GetAPYMonthlyRepayment(decimal requestedAmount, decimal interestRateInPercent, int loanTerm)
        {           
            var apy = Math.Pow((double)(1+interestRateInPercent/12), 12) - 1;

            return (apy / (1 - Math.Pow(1 + apy, -loanTerm))* (double)requestedAmount);
            
                
                
            //    var a = ((double)requestedAmount * (double)apy * Math.Pow((double)(1 + apy), loanTerm));
            //var b = Math.Pow((double)(1 + apy), loanTerm) - 1;
           // var apr = a / b;

            //return apr;
        }
    }
}
