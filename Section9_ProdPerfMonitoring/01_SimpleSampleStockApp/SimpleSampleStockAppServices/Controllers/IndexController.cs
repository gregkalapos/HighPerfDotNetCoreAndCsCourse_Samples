using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimlpeSampleStockAppModel;
using SimpleSampleStockAppServices.Data;

namespace SimpleSampleStockAppServices.Controllers
{
    [Route("api/[controller]")]
    public class IndexController : Controller
    {
        private readonly SimpleStockAppDataContext dataContext;

        public IndexController(SimpleStockAppDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        // GET api/values
        [HttpGet]
        public List<StockIndexData> Get()
        {
            var retVal = new List<StockIndexData>();
            var indexList = dataContext.StockIndexes.ToList();

            foreach (var item in indexList)
            {
                retVal.Add(new StockIndexData { Name = item.Name, Symbol = item.Symbol });
            }

            return retVal;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public List<SimpleStockData> Get(string id)
        {
            var retVal = new List<SimpleStockData>();
            var idUpper = id.ToUpper();
            var stockList = dataContext.StockStockIndexes.Include(n=>n.StockIndex).Where(n => n.StockIndex.Symbol == idUpper).Include(n=>n.Stock);

            foreach (var item in stockList)
            {
                retVal.Add(new SimpleStockData { Symbol = item.Stock.Symbol, CompanyName = item.Stock.Name });
            }
            
            return retVal;
        }
    }
}
