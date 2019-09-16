using System;
using System.Net;
using System.Text.RegularExpressions;

namespace SolidTest.Controls
{
    public class CBRWebData
    {
        protected string _currencyUrl;
        protected string _rateUrl;
        public string RateData;
        public string CurrencyData;
        public event EventHandler<ErrorEventArgs> onError;
        protected string Date { get; set; }
        public CBRWebData(DateTime date)
        {
            Date = date.ToString("dd.MM.yyyy");
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

        /// <summary>
        /// Получаем справочник валют и котировки на заданную дату
        /// </summary>
        public void GetData()
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    this.CurrencyData = client.DownloadString(_currencyUrl);
                    this.RateData = client.DownloadString(_rateUrl);
                }
            }
            catch (Exception e)
            {
                onError(this, new ErrorEventArgs(6));
            }
            if (this.CurrencyData == null)
                onError(this, new ErrorEventArgs(0));
            if (this.RateData == null)
                onError(this, new ErrorEventArgs(1));



        }


    }
}
