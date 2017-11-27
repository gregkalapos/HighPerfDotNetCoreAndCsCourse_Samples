using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleSampleStockAppServices.Data
{
    /// <summary>
    /// Conntects stocks to indexes and indexes to stocks
    /// (many-to-many)
    /// </summary>
    public class StockStockIndex
    {
        public int Id { get; set; }

        public Stock Stock { get; set; }
        
        public StockIndex StockIndex { get; set; }
    }
}
