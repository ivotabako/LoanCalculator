using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zopa.LoanCalculator.Core;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace Zopa.LoanCalculator.Client
{
    
    public class Program
    {
       
        static void Main(string[] args)
        {
            LendersManager csvLoader = new LendersManager(File);

            var lenders = csvLoader.Load(args[0]);

            Core.LendersManager manager = new Core.LendersManager(lenders);
            var quote = manager.GetBestQuote(decimal.Parse( args[1]), 36);



        }
    }
}
