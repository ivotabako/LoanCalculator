using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zopa.LoanCalculator.Core
{
    public class Lender
    {
        public string Name { get; set; }
        public double Rate { get; set; }
        public double AvailableAmount { get; set; }
    }
}
