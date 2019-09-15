using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidTest.Data
{
    public class CBRCurrency : CBR
    {
        public string Name { get; private set; }
        public string EngName { get; private set; }
        public string ParentCode { get; private set; }
        

        public CBRCurrency(string id, string name, string engName, int nominal, string parentCode)
        {
            Id = id;
            Name = name;
            EngName = engName;
            Nominal = nominal;
            ParentCode = parentCode;
        }

    }
}
