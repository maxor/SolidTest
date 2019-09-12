using SolidTest.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SolidTest.Controls
{
    public class RateParser : CBRXMLParser
    {
        public RateParser(string xml) : base(xml)
        {
            Rates = new List<CBRRate>();
        }
        public void DocParser()
        {
            XmlNodeList list =  _document.SelectNodes("ValCurs/Valute");
            foreach (XmlNode node in list)
            {
                Rates.Add(new CBRRate(
                   node.Attributes["ID"].Value,
                   DateTime.Now,
                    Convert.ToInt32(node.SelectSingleNode("NumCode").InnerText),
                    node.SelectSingleNode("CharCode").InnerText,
                    node.SelectSingleNode("Nominal").InnerText,
                    node.SelectSingleNode("Name").InnerText, 
                    Convert.ToDouble(node.SelectSingleNode("Value").InnerText)
                    ));
            }
        }
    }
}
