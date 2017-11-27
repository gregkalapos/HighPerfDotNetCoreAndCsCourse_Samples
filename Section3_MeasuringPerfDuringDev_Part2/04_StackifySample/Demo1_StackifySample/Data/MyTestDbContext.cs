using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo1_StackifySample.Data
{
    public class MyTestDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"ADD_YOUR_CONNECTIONSTRING_HERE");
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
