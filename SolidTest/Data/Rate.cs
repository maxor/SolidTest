using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidTest.Data
{
    public class Rate
    {
        public int RateID { get; set; }
        public DateTime Date { get; set; }
        public int Nominal { get; set; }
        public double Value { get; set; }
        public Currency CurrencyID { get; set; }
    }
}
