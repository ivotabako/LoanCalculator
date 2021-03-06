﻿using System;
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
            int loanTerm = 36;
            int numberOfPaymentsPerYear = 12;

            var fileReader = new FileReader();

            var inputIsValid = new InputValidator(fileReader).IsInputValid(args);

            if (!inputIsValid)
            {
                Console.WriteLine("The provided filename or the loan amount is invalid!");
                Console.WriteLine("Ex.: cmd> quote.exe [market_file] [loan_amount]");
                Console.WriteLine("Please press a button to exit");
                Console.ReadLine();
                Environment.Exit(1);
            }

            var fileName = args[0];
            int loanAmount = int.Parse( args[1]);

            LendersManager lendersMgr = new LendersManager(fileReader, new Calculator(), fileName);
            
            var quote = lendersMgr.GetBestQuote(loanAmount, loanTerm, numberOfPaymentsPerYear);

            quote.Print(Console.Write);

            Console.ReadLine();

        }
    }
}
