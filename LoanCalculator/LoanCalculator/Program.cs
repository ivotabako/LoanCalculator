using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zopa.LoanCalculator.Core;

namespace Zopa.LoanCalculator.Client
{
    
    public class Program
    {
       
        static void Main(string[] args)
        {
            var fileReader = new FileReader();

            var inputIsValid = new InputValidator(fileReader).IsInputValid(args);

            if (!inputIsValid)
            {
                Console.WriteLine("Then filename or the loan amount are wrong!");
                Console.WriteLine("Please press a button to exit");
                Console.ReadLine();
                Environment.Exit(1);
            }

            LendersManager csvLoader = new LendersManager(fileReader, new Calculator(), args[0]);
            
            var quote = csvLoader.GetBestQuote(decimal.Parse( args[1]), 36);

            quote.Print(Console.Write);

        }
    }
}
