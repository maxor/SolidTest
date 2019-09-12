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


        public CBRXMLParser(string xml)
        {
            _document = CheckXML(xml);
        }

        private XmlDocument CheckXML(string xml)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(xml);
                return doc;
            }
            catch
            {
                return null;
            }
        }


    }
}
