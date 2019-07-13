﻿using Microsoft.EntityFrameworkCore;
using NPV.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NPV.DAL
{
    public class NPVContext : DbContext
    {
        public NPVContext() : base(GetOptions(Constants.DBContextName)) { }

        private static DbContextOptions GetOptions(string connString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connString).Options;
        }

        public DbSet<NPVCalculations> NPVCalculations { get; set; }
        public DbSet<NPVCalculation> NPVCalculation { get; set; }
        public DbSet<Cashflow> Cashflow { get; set; }
        public DbSet<Parameters> Parameters { get; set; }
    }

}
