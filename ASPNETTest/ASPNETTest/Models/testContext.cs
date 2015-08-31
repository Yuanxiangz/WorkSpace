using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using ASPNETTest.Models.Mapping;

namespace ASPNETTest.Models
{
    public partial class testContext : DbContext
    {
        static testContext()
        {
            Database.SetInitializer<testContext>(null);
        }

        public testContext()
            : base("Name=testContext")
        {
        }

        public DbSet<table1> table1 { get; set; }
        public DbSet<table2> table2 { get; set; }
		public DbSet<table3> table3 { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new table1Map());
            modelBuilder.Configurations.Add(new table2Map());
			modelBuilder.Configurations.Add(new table3Map());
        }
    }
}
