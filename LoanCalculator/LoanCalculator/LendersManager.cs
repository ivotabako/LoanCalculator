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
    public class LendersManager
    {        
        private readonly long _bigSize;
        private readonly IFileReader _fileReader;

        public LendersManager(IFileReader fileReader,
            long bigSize = 5000000 )
        {
            _fileReader = fileReader;
            _bigSize = bigSize;
        }

        public List<Lender> Load(string fileName)
        {
            return new FileInfo(fileName).Length > _bigSize ? LoadFromBigFile(fileName) : LoadFromSmallFile(fileName);
        }

        private List<Lender> LoadFromSmallFile(string fileName)
        {
            List<Lender> result = new List<Lender>();
            _fileReader.ReadLines(fileName).Skip(1).ToList().ForEach(m => {
                var fielnds = m.Split(',');
                result.Add(new Lender() {
                    Name = fielnds[0],
                    Rate = Decimal.Parse(fielnds[1]),
                    AvailableAmount = Decimal.Parse(fielnds[2])
                });
            });

            return result;
        }

        private List<Lender> LoadFromBigFile(string fileName)
        {
            List<Lender> result = new List<Lender>();
            try
            {
                using (CsvReader csv = new CsvReader(new StreamReader(fileName), true))
                {
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
                LogManager.GetLogger("LoanCalculator").Error(ex.Message);
            }

            return result;
        }
    }
}
