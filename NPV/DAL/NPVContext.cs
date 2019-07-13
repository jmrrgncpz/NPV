using NPV.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace NPV.DAL
{
    public class NPVContext : DbContext
    {
        public NPVContext() : base(Constants.DBContextName)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<NPVCalculation>()
                .HasRequired<NPVCalculations>(s => s.NPVCalculations)
                .WithMany(t => t.ResultSet)
                .HasForeignKey(t => t.NPVCalculationsID);

            modelBuilder.Entity<Cashflow>()
                .HasRequired<NPVCalculations>(r => r.NPVCalculations)
                .WithMany(npvc => npvc.Cashflows)
                .HasForeignKey(c => c.NPVCalculationsID);
        }

        public DbSet<NPVCalculations> NPVCalculations { get; set; }
        public DbSet<NPVCalculation> NPVCalculation { get; set; }
        public DbSet<Cashflow> Cashflow { get; set; }
    }

}
