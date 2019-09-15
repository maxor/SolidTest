using SolidTest.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidTest.Data
{
    public class DBMethods
    {
        public void SaveCurrency(Currency currencies)
        {
            using (SolidContext sc = new SolidContext())
            {
                Currency cur = (from c in sc.Currencies
                           where c.CharCode == currencies.CharCode
                           select c).SingleOrDefault();
                if(cur == null)
                {
                    sc.Currencies.Add(currencies);
                    sc.SaveChanges();
                }
                else
                {
                    RenewRates(currencies, currencies.Rates.Single().Date);
                }
                
            }
        }
        private void RenewRates(Currency currencies, DateTime date)
        {
            using (SolidContext sc = new SolidContext())
            {
                Currency cur = sc.Currencies.Include("Rates").Where(c => c.CharCode == currencies.CharCode).FirstOrDefault();
                Rate rate = cur.Rates.Where(d => d.Date == date.Date).FirstOrDefault();
                if (cur.Rates.Count() == 0 | rate == null)
                {
                    foreach (var item in currencies.Rates)
                    {
                        item.CurrencyID = cur;
                        sc.Rates.Add(item);
                    }
                    sc.SaveChanges();
                }
                else
                {
                    foreach (Rate item in currencies.Rates)
                    {
                        rate.Value = item.Value;
                        sc.SaveChanges();
                    }
                }
            }
        }

    }
}
