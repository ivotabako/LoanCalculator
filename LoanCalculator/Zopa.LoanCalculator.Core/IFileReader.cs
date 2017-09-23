using System;
using System.Collections.Generic;

namespace Zopa.LoanCalculator.Core
{
    /// <summary>
    /// used tto abstract out the file system access
    /// 
    /// </summary>
    public interface IFileReader
    {
        IEnumerable<string> ReadLines(string fileName);

        bool Exists(string fileName);
    }
}