using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidTest.Data
{
    public class CBRRate : CBR
    {
        /*
        <Valute ID="R01010">
            <NumCode>036</NumCode>
            <CharCode>AUD</CharCode>
            <Nominal>1</Nominal>
            <Name>Австралийский доллар</Name>
            <Value>44,8914</Value>
        </Valute>
        */
        public CBRRate(string id, DateTime date, int numCode, string charCode, int nominal, string name, double value)
        {
            Id = id;
            NumCode = numCode;
            CharCode = charCode;
            Nominal = nominal;
            Name = name;
            Value = value;
            Date = date;
        }
        public int NumCode { get; private set; }
        public string CharCode { get; private set; }
        public string Name { get; private set; }
        public double Value { get; private set; }
        public DateTime Date { get; set; }
    }
}
