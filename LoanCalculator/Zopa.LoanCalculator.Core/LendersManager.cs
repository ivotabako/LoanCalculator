using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zopa.LoanCalculator.Core
{
    public class LendersManager
    {
        private readonly List<Lender> _lenders;

        public LendersManager(List<Lender> lenders)
        {
            _lenders = lenders;
        }

        public LoanOffer GetBestQuote(decimal loanAmmount, int loanTerm)
        {
            Lender bestLender = _lenders.Where(m => m.AvailableAmount >= loanAmmount).OrderBy(m => m.Rate).FirstOrDefault();
            Calculator calc = new Calculator();
            var monthlyRepayment = calc.GetAPYMonthlyRepayment(loanAmmount, bestLender.Rate, loanTerm);            
            return bestLender != null ? new LoanOffer(bestLender.Rate, (decimal)monthlyRepayment) : new LoanOffer(0,0);
        }
    }
}
