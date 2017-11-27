using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimlpeSampleStockAppModel
{
    public class StockData
    {
        public String Symbol { get; set; }
        public Decimal LastValue { get; set; }

        public Double ChangeInPercent { get; set; }

        public Decimal Change { get; set; }

        public String CompanyName { get; set; }

        public Decimal? DaysLow { get; set; }

        public Decimal? DaysHigh { get; set; }

        public String Currency { get; set; }
    }
}