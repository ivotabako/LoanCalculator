using System;
using System.IO;

namespace Zopa.LoanCalculator.Core
{
    public class InputValidator
    {
        private readonly IFileReader _fileReader;

        public InputValidator(IFileReader fileReader)
        {
            _fileReader = fileReader;
        }

        public bool IsInputValid(string[] arguments)
        {
            return arguments.Length == 2 && DoesFileExists(arguments[0]) && IsLoanAmountValid(arguments[1]);
        }

        private bool IsLoanAmountValid(string loanAmount)
        {
            int dummy;
            return int.TryParse(loanAmount, out dummy) && LoanIsInRange(dummy);
        }

        private bool LoanIsInRange(int loan)
        {
            return 1000 <= loan && loan <= 15000 && loan % 100 == 0;
        }

        private bool DoesFileExists(string fileName)
        {
            return _fileReader.Exists(fileName);
        }
    }
}