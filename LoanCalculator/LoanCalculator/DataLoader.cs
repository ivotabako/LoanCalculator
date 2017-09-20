using log4net;
using LumenWorks.Framework.IO.Csv;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zopa.LoanCalculator.Core;

namespace Zopa.LoanCalculator.Client
{
    public class DataLoader
    {
        private readonly ILog _logger;

        public DataLoader(ILog logger)
        {
            _logger = logger;
        }

        public List<Lender> LoadFromCSV(string fileName)
        {
            List<Lender> result = new List<Lender>();
            try
            {
                using (CsvReader csv = new CsvReader(new StreamReader(fileName), true))
                {
                    int fieldCount = csv.FieldCount;

                    while (csv.ReadNextRecord())
                    {
                        result.Add(new Lender()
                        {
                            Name = csv["Lender"],
                            Rate = Decimal.Parse(csv["Rate"]),
                            AvailableAmount = Decimal.Parse(csv["Available"])
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

                return new List<Lender>();
            }

            return result;
        }
    }
}
