using SolidTest.Controls;
using SolidTest.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidTest
{
    class Program
    {
        static void Main(string[] args)
        {
            CBRWebData cbr = new CBRWebData();
            cbr.GetData();
            RateParser rate = new RateParser(cbr.RateData);
            rate.DocParser();
            CurrencyParser currency = new CurrencyParser(cbr.CurrencyData);
            currency.DocParser();
            

            Console.ReadKey();

        }
    }
}
