using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimlpeSampleStockAppModel;
using SimpleSampleStockAppServices.HistoricalData;

namespace SimpleSampleStockAppServices.Controllers
{
    [Route("api/[controller]")]
    public class HistoricalDataController : Controller 
    {
      
        [HttpGet("{id}")]
        public IEnumerable<HistoricalValue> Get(string id)
        {
            var hps = new HistoricalPriceStorage(@"HistoricalData\DataFiles");
            var res = hps.GetHistoricalQuotes(id).Where(n => n.Date > new DateTime(2005, 1, 1)).Where((x, i) => i % 10 == 0).OrderBy(n=>n.Date);
            return res;
        }
    }
}
