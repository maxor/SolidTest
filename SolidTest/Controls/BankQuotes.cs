using SolidTest.Controls;
using SolidTest.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidTest.Controls
{
    public class BankQuotes
    {
        DateTime _date;
        CBRWebData _request;
        CBRXMLParser _parser;
        public event EventHandler<ErrorEventArgs> OnError;
        /// <summary>
        /// Базовый конструктор для получения котировок с cbr.ru
        /// </summary>
        /// <param name="date">Дата для запроса</param>
        public BankQuotes(DateTime date)
        {
            _date = date;
           
        }

        private void onBankQoutes(object sender, ErrorEventArgs e)
        {
            OnError(this, e);
        }

       

        public void GetQuotes()
        {
            if (_request == null)
                _request = new CBRWebData(_date);
            if (_parser == null)
                _parser = new CBRXMLParser();
            _request.onError += onBankQoutes;
            _parser.onError += onBankQoutes;
            _request.GetData();
                _parser.ParseXML(CBRXMLTypes.Currency, _request.CurrencyData);
                _parser.ParseXML(CBRXMLTypes.Rate, _request.RateData);
                UpdateDB();
                GenerateExcelFile();
        }
        protected void GenerateExcelFile()
        {
            try
            {
                ExcelGenerator eg = new ExcelGenerator(_date);
                eg.GenerateExcelFile(_parser);
            }
            catch (Exception e)
            {
                OnError(this, new ErrorEventArgs(8));
            }
        }

        protected List<Currency> SetCurrency()
        {
            List<Currency> curList = new List<Currency>();
            foreach (var currency in _parser.Currency)
            {
                List<CBRRate> rt = GetRates(currency);
                List<Rate> rtList = new List<Rate>();
                foreach (CBRRate rate in rt)
                {
                    rtList.Add(new Rate() { Date = _date.Date, Nominal = rate.Nominal, Value = rate.Value });
                }
                try
                {
                    Currency cur = new Currency()
                    {
                        Id = currency.Id,
                        NumCode = (from r in _parser.Rates
                                   where r.Id == currency.Id
                                   select r.NumCode).FirstOrDefault().ToString(),
                        CharCode = (from r in _parser.Rates
                                    where r.Id == currency.Id
                                    select r.CharCode).FirstOrDefault().ToString(),
                        Rates = rtList
                    };
                    curList.Add(cur);
                }
                catch
                {}
                
            }
            return curList;
        }

        protected List<CBRRate> GetRates(CBRCurrency currency)
        {
            return (from b in _parser.Rates
                    where b.Id == currency.Id
                    select b).ToList();
        }

        protected void UpdateDB()
        {
            DBMethods db = new DBMethods();
            List<Currency> list = SetCurrency();
            foreach (Currency cur in list)
            {
                db.SaveCurrency(cur);
            }
        }

    }
}
