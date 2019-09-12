namespace SolidTest.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class SolidContext : DbContext
    {
        public SolidContext()
            : base("name=SolidContext")
        {
        }

        public virtual DbSet<CBRRate> CBRRates { get; set; }
        public virtual DbSet<CBRCurrency> CBRCurrency { get; set; }
    }
}