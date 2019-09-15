using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidTest.Data
{
    public class Currency
    {
        [Key]
        public int CurrencyID { get; set; }
        public string Id { get; set; }
        public string NumCode { get; set; }
        public string CharCode { get; set; }
        public List<Rate> Rates { get; set; }

        public Currency()
        {
            Rates = new List<Rate>();
        }


    }
}
