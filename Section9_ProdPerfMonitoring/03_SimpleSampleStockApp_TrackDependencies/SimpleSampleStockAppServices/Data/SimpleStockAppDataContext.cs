using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SimpleSampleStockAppServices.Data
{
    public class SimpleStockAppDataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"[YOUR-CONNECTION-STRING]");
        }

        public DbSet<StockIndex> StockIndexes { get; set; }

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<StockStockIndex> StockStockIndexes{ get; set; }

        public DbSet<TradingQuotes> StockQuotes { get; set; }

        
    }   
}
