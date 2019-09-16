using SolidTest.Controls;
using SolidTest.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SolidTest.Controls
{
    public class CBRXMLParser
    {
        protected XmlDocument _document { get; private set; }
        public List<CBRCurrency> Currency;
        public List<CBRRate> Rates;
        public event EventHandler<ErrorEventArgs> onError;
        public CBRXMLParser()
        {
            _document = new XmlDocument();
        }

        private void CheckXML(string xml)
        {
            try
            {
                _document.LoadXml(xml);
            }
            catch(Exception e)
            {
                onError(this, new ErrorEventArgs(7));
            }
        }

        public void ParseXML(CBRXMLTypes type, string xml)
        {
            CheckXML(xml);
            switch (type)
            {
                case CBRXMLTypes.Currency:
                    ParseCurrency();
                    break;
                case CBRXMLTypes.Rate:
                    ParseRates();
                    break;
                default:
                    break;
            }
        }
        
        protected void ParseRates()
        {
            if (Rates == null)
                Rates = new List<CBRRate>();
            else
                Rates.Clear();
            XmlNodeList list = _document.SelectNodes("ValCurs/Valute");
            try
            {
                foreach (XmlNode node in list)
                {
                    Rates.Add(new CBRRate(
                       node.Attributes["ID"].Value,
                       DateTime.Now,
                        Convert.ToInt32(node.SelectSingleNode("NumCode").InnerText),
                        node.SelectSingleNode("CharCode").InnerText,
                        Convert.ToInt32(node.SelectSingleNode("Nominal").InnerText),
                        node.SelectSingleNode("Name").InnerText,
                        Convert.ToDouble(node.SelectSingleNode("Value").InnerText)
                        ));
                }
            }
            catch (Exception e)
            {
                onError(this, new ErrorEventArgs(3));
            }
            
        }

        protected void ParseCurrency()
        {
            if (Currency == null)
                Currency = new List<CBRCurrency>();
            else
                Currency.Clear();
            XmlNodeList list = _document.SelectNodes("Valuta/Item");
            try
            { 
                foreach (XmlNode node in list)
                {
                    Currency.Add(new CBRCurrency(
                       node.Attributes["ID"].Value,
                        node.SelectSingleNode("Name").InnerText,
                        node.SelectSingleNode("EngName").InnerText,
                        Convert.ToInt32(node.SelectSingleNode("Nominal").InnerText),
                        node.SelectSingleNode("ParentCode").InnerText
                        ));
                }
            }
            catch (Exception e)
            {
                onError(this, new ErrorEventArgs(2));
            }

}

    }
    public enum CBRXMLTypes
    {
        Currency,
        Rate
    }
}
