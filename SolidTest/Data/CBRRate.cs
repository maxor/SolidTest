using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidTest.Data
{
    public class CBRRate
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
        public CBRRate(string id, int numCode, string charCode, string nominal, string name, double value)
        {
            ID = id;
            NumCode = numCode;
            ChaeCode = charCode;
            Nominal = nominal;
            Name = name;
            Value = value;
        }

        public string ID { get; private set; }
        public int NumCode { get; private set; }
        public string ChaeCode { get; private set; }
        public string Nominal { get; private set; }
        public string Name { get; private set; }
        public double Value { get; private set; }
    }
}
