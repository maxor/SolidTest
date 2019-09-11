using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidTest.Data
{
    public class CBRCurrency
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string EngName { get; private set; }
        public string Nominal { get; private set; }
        public string ParentCode { get; private set; }

        public CBRCurrency(string id, string name, string engName, string nominal, string parentCode)
        {
            Id = id;
            Name = name;
            EngName = engName;
            Nominal = nominal;
            ParentCode = parentCode;
        }

    }
}
