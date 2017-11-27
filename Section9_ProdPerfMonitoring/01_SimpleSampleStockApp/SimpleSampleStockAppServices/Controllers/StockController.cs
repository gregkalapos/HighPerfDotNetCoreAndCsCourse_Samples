using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            //TODO: Db Call
            return new StockData
            {
                Change = quoteItem.Change, // 23.2m,
                ChangeInPercent = quoteItem.ChangeInPercent,// 23,
                DaysHigh = quoteItem.DaysHigh, // 21,
                DaysLow = quoteItem.DaysLow, // 21,
                CompanyName = stockItem.Name, //"sdfsdfsddf  sdf",
                Currency = stockItem.Currency,
                LastValue = quoteItem.LastValue, // 23,
                Symbol = idUpper
            };
        }
    }
}
