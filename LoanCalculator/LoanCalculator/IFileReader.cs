using System.Collections.Generic;

namespace Zopa.LoanCalculator.Client
{
    public interface IFileReader
    {
        IEnumerable<string> ReadLines(string fileName);
    }
}