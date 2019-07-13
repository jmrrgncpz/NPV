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

            modelBuilder.Entity<SingleNPVCalculation>()
                .HasRequired<Calculation>(s => s.Calculation)
                .WithMany(t => t.ResultSet)
                .HasForeignKey(t => t.CalculationID);

            modelBuilder.Entity<Cashflow>()
                .HasRequired<Calculation>(r => r.Calculation)
                .WithMany(npvc => npvc.Cashflows)
                .HasForeignKey(c => c.CalculationID);
        }

        public DbSet<Calculation> NPVCalculations { get; set; }
        public DbSet<SingleNPVCalculation> NPVCalculation { get; set; }
        public DbSet<Cashflow> Cashflow { get; set; }
    }

}
