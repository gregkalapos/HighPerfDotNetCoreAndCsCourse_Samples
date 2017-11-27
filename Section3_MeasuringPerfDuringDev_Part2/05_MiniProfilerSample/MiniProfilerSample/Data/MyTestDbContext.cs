using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniProfilerSample.Data
{
    public class MyTestDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Portfolio.db");
        }

        public DbSet<UserPortfolioItem> UserPortfolioItems { get; set; }
    }

    public class UserPortfolioItem
    {
        public int Id { get; set; }
        public String CurrencyName { get; set; }

        public Decimal Amount { get; set; }
    }
}
