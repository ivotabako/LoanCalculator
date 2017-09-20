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
        public decimal Rate { get; set; }
        public decimal AvailableAmount { get; set; }
    }
}
