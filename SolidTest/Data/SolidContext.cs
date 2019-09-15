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
        public virtual DbSet<Rate> Rates { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
    }
}