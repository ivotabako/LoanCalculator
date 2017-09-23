using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zopa.LoanCalculator.Core;

namespace Zopa.LoanCalculator.Core
{
    public class LendersManager
    {
        private readonly ICalculator _calculator;
        private readonly IFileReader _fileReader;
        private readonly List<Lender> _lenders;

        private bool _hasError;
        private string _errorMessage;

        public LendersManager(IFileReader fileReader, ICalculator calculator, string fileName)           
        {
            _fileReader = fileReader;
            _lenders = Load(fileName);
            _calculator = calculator;        
        }

        private List<Lender> Load(string fileName)
        {
            var lenders = new List<Lender>();
            try
            {
                _fileReader.ReadLines(fileName).Skip(1).ToList().ForEach(m => {
                    var fielnds = m.Split(',');
                    lenders.Add(new Lender()
                    {
                        Name = fielnds[0],
                        Rate = Decimal.Parse(fielnds[1]),
                        AvailableAmount = Decimal.Parse(fielnds[2])
                    });
                });
            }
            catch (Exception ex)
            {
                _hasError = true;
                _errorMessage = ex.Message;
                lenders.Clear();
            }           

            return lenders;
        }

        public LoanOffer GetBestQuote(decimal loanAmount, int loanTerm)
        {
            if (_hasError)
            {
                return LoanOffer.SetError(_errorMessage);
            }

            Lender bestLender = _lenders.Where(m => m.AvailableAmount >= loanAmount).OrderBy(m => m.Rate).FirstOrDefault();     
            if(bestLender != null)
            {
                var monthlyRepayment = _calculator.GetAPYMonthlyRepayment(loanAmount, bestLender.Rate, loanTerm);
                return new LoanOffer(bestLender.Rate, monthlyRepayment);
            }

            return new LoanOffer(0,0);
        }

    }
}
