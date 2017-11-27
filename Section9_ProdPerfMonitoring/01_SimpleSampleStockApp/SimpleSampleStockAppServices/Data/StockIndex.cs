using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleSampleStockAppServices.Data
{
    public class StockIndex
    {
        [Key]
        public String Symbol { get; set; }
        public String Name { get; set; }

        public Boolean IsOpen { get; set; }

        public List<StockStockIndex> StockStockIndex { get; set; }

        /// <summary>
        /// The currency of the Index. E.g. EUR, USD, GBP, etc. 
        /// </summary>
        public String Currency { get; set; }
    }
}
