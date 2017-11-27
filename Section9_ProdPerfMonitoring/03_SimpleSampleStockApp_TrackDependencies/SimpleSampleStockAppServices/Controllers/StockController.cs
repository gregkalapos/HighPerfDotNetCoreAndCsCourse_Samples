using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using SimlpeSampleStockAppModel;
using SimpleSampleStockAppServices.Data;
using SimpleSampleStockAppServices.HistoricalData;

namespace SimpleSampleStockAppServices.Controllers
{
    [Route("api/[controller]")]
    public class StockController : Controller 
    {
        private readonly SimpleStockAppDataContext simpleStockAppDataContext;
        private TelemetryClient telemetryClient = new TelemetryClient();
        public StockController(SimpleStockAppDataContext simpleStockAppDataContext)
        {
            this.simpleStockAppDataContext = simpleStockAppDataContext;
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public StockData Get(string id)
        {           
            var idUpper = id.ToUpper();
            var quoteItem = simpleStockAppDataContext.StockQuotes.Where(n => n.Symbol == idUpper).Single();
            var stockItem = simpleStockAppDataContext.Stocks.Where(n => n.Symbol == idUpper).Single();

            telemetryClient
               .TrackEvent("Stock details requested",
                       new Dictionary<string, string> { { "StockSymbol", id } });

            return new StockData
            {
                Change = quoteItem.Change,
                ChangeInPercent = quoteItem.ChangeInPercent,
                DaysHigh = quoteItem.DaysHigh,
                DaysLow = quoteItem.DaysLow,
                CompanyName = stockItem.Name,
                Currency = stockItem.Currency,
                LastValue = quoteItem.LastValue,
                Symbol = idUpper
            };
        }
    }
}
