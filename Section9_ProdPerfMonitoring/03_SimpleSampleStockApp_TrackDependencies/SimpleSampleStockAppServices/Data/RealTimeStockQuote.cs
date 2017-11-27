using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleSampleStockAppServices.Data
{
    public class TradingQuotes
    {
        [Key]
        public String Symbol { get; set; }

        private decimal lastValue;

        public Decimal LastValue
        {
            get { return Math.Round(lastValue, 2); }
            set { lastValue = value; }
        }

        public Double ChangeInPercent { get; set; }

        public Double? PERatio { get; set; }

        public decimal? DaysLow { get; set; }

        public decimal? DaysHigh { get; set; }

        public decimal? YearsLow { get; set; }

        public decimal? YearsHigh { get; set; }

        public decimal? Bid { get; set; }

        public decimal? Ask { get; set; }

        public long? Volume { get; set; }

        private decimal change;

        /// <summary>
        /// Absolute change 
        /// </summary>
        public decimal Change
        {
            get { return Math.Round(change, 2); }
            set { change = value; }
        }

        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        /// The opening price for the current day
        /// </summary>
        public Decimal Open { get; set; }

        /// <summary>
        /// The Time and Date of the last trade repodted by the data source
        /// </summary>
        public DateTime? LastTradeDateTime { get; set; }
    }
}
