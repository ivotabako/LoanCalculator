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
            return DoesFileExists(arguments[0]) && IsLoanAmountProvided(arguments[1]);

        }

        private bool IsLoanAmountProvided(string loanAmount)
        {
            decimal dummy;
            return decimal.TryParse(loanAmount, out dummy);
        }

        private bool DoesFileExists(string fileName)
        {
            return _fileReader.Exists(fileName);
        }
    }
}