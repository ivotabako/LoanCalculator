using System;
using System.Collections.Generic;
using System.IO;

namespace Zopa.LoanCalculator.Core
{
    /// <summary>
    /// Wrapper around File class and its ReadLines and Exists methods
    /// </summary>
    public class FileReader : IFileReader
    {
        public IEnumerable<string> ReadLines(string fileName)
        {
            try
            {
                return File.ReadLines(fileName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }           
        }

        public bool Exists(string fileName)
        {
            return File.Exists(fileName);
        }
    }
}