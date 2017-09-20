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
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
   
        static void Main(string[] args)
        {
            DataLoader csvLoader = new DataLoader(log);

            var lenders = csvLoader.LoadFromCSV(args[0]);

            LendersManager manager = new LendersManager(lenders);
            var quote = manager.GetBestQuote(decimal.Parse( args[1]), 36);



        }
    }
}
