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
            DateTime _date = ParseDate(args);
            BankQuotes bq = new BankQuotes(_date);
            bq.OnError += Bq_OnError;
            bq.GetQuotes();
        }

        private static void Bq_OnError(object sender, ErrorEventArgs e)
        {
            Console.Write(e.Message);
            Console.WriteLine();
            Console.ReadKey();
        }

        static DateTime ParseDate(string[] args)
        {
            DateTime result;
            if (args.Count() > 0)
                return (DateTime.TryParse(args[0], out result)) ? result : DateTime.Now;
            else
            {
                Console.Write("Введите дату в формате 'dd.mm.yyyy'");
                return (DateTime.TryParse(Console.ReadLine(), out result)) ? result : DateTime.Now;
            }
            
        }
    }
}
