using System;
using System.Collections.Generic;
using System.IO;

namespace Zopa.LoanCalculator.Client
{
    internal class FileReader : IFileReader
    {
        public IEnumerable<string> ReadLines(string fileName)
        {
            return File.ReadLines(fileName);
        }
    }
}