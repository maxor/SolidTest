using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SolidTest.Data
{
    public class CBRWebData
    {
        protected string _currencyUrl;
        protected string _rateUrl;
        public string RateData;
        public string CurrencyData;
        public string Date { get; private set; }
        public CBRWebData()
        {
            Date = DateTime.Now.ToString(@"dd/MM/yyyy");
            SetBase();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date">date format - dd.mm.yyyy</param>
        public CBRWebData(string date)
        {
            Date = CheckDate(date);
            SetBase();
        }
        
        string CheckDate(string date)
        {
            Regex rgx = new Regex(@"^\d{2}.\d{2}.\d{4}");
            return rgx.IsMatch(date) ? date.Replace('.', '/') : String.Empty;
        }

        void SetBase()
        {
            _currencyUrl = @"http://www.cbr.ru/scripts/XML_val.asp?d=0";
            _rateUrl = @"http://www.cbr.ru/scripts/XML_daily.asp?date_req=" + Date;
        }

        void GetRate()
        {
            using (WebClient client = new WebClient())
            {
                this.RateData = client.DownloadString(_rateUrl);
            }
        }

        void GetCurrency()
        {
            using (WebClient client = new WebClient())
            {
                this.CurrencyData = client.DownloadString(_currencyUrl);
            }
        }

        public void GetData()
        {
            GetRate();
            GetCurrency();
        }


    }
}
