using SolidTest.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SolidTest.Controls
{
    public class CurrencyParser : CBRXMLParser
    {
        List<CBRCurrency> Currency;
        public CurrencyParser(string xml) : base(xml)
        {
            Currency = new List<CBRCurrency>();
        }

        public void DocParser()
        {
            XmlNodeList list = _document.SelectNodes("Valuta/Item");
            foreach (XmlNode node in list)
            {
                Currency.Add(new CBRCurrency(
                   node.Attributes["ID"].Value,
                    node.SelectSingleNode("Name").InnerText,
                    node.SelectSingleNode("EngName").InnerText,
                    node.SelectSingleNode("Nominal").InnerText,
                    node.SelectSingleNode("ParentCode").InnerText
                    ));
            }
        }
    }
}
