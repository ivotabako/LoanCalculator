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
        /// Gets the monthly payments amount using annual percentage yield formula 
        /// </summary>
        public double GetMonthlyPayment(int requestedAmount, double interestRateInPercent, int loanTerm, int numberOfPaymentsPerYear)
        {
            var nominalAnnuealInterestRate = (Math.Pow(1.0 + interestRateInPercent, 1.0 / numberOfPaymentsPerYear) - 1.0) * numberOfPaymentsPerYear;

            var periodicInterestRate = nominalAnnuealInterestRate / numberOfPaymentsPerYear;

            var discountFactorNumerator = Math.Pow(1.0 + periodicInterestRate, loanTerm) - 1.0;

            var discountFactorDenominator = (periodicInterestRate * Math.Pow(1 + periodicInterestRate, loanTerm));

            var discountFactor = discountFactorNumerator / discountFactorDenominator;

            return requestedAmount / discountFactor;           
        }
    }
}
