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
            string _date;
            //date format 20.12.2019
            if (args.Count() > 0)
                _date = args[0];
            else
            {
                Console.Write("Введите дату в формате 'dd.mm.yyyy'");
                _date = Console.ReadLine();
            }
            CBRWebData cbr = new CBRWebData(_date);
            cbr.GetData();
            RateParser rate = new RateParser(cbr.RateData);
            rate.DocParser();
            ExcelGenerator eg = new ExcelGenerator(_date);
            eg.GenerateExcelFile(rate);


           // CurrencyParser currency = new CurrencyParser(cbr.CurrencyData);
           // currency.DocParser();
            //Console.ReadKey();
        }
    }
}
