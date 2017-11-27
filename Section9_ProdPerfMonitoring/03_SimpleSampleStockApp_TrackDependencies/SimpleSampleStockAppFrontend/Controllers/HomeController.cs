using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SimlpeSampleStockAppModel;
using SimpleSampleStockAppFrontend.Models;

namespace SimpleSampleStockAppFrontend.Controllers
{
    public class HomeController : Controller
    {
        private HttpClient httpClient = new HttpClient();

        public async Task<IActionResult> Index()
        {
            var res = await httpClient.GetAsync(Consts.SERVICESURL + "Index");
            var indexList = JsonConvert.DeserializeObject<List<StockIndexData>>(await res.Content.ReadAsStringAsync());
            return View(indexList);
        }

        [HttpGet("{id}")]
        public  async Task<IActionResult> StocksInIndex(String id)
        {
            ViewData["IndexId"] = id;
            var res = await httpClient.GetAsync(Consts.SERVICESURL + "Index/" + id);
            var stockList = JsonConvert.DeserializeObject<List<SimpleStockData>>(await res.Content.ReadAsStringAsync());
            return View(stockList);
        }

        public async Task<IActionResult> StockData(String id)
        {
            ViewData["StockSymbol"] = id;
            var res = await httpClient.GetAsync(Consts.SERVICESURL + "Stock/" + id);
            var stockData = JsonConvert.DeserializeObject<StockData>(await res.Content.ReadAsStringAsync());

            if(stockData.Currency == "USD")
            {
                var currencyRates = await GetRates();
                var valueInEur = stockData.LastValue / (decimal)currencyRates.Rates.USD;
                ViewData["ValueInEur"] = valueInEur.ToString("#.##");
            }

            res = await httpClient.GetAsync(Consts.SERVICESURL + "HistoricalData/" + "msft"); //TODO

            var historicalData = JsonConvert.DeserializeObject<List<HistoricalValue>>(await res.Content.ReadAsStringAsync());
            ViewData["HistoricalData"] = historicalData;



            return View(stockData);
        }

        /// <summary>
        /// Do an HTTP call to get the latest currency rates and deserialize it with json.net
        /// </summary>
        /// <returns></returns>
        private async Task<CurrencyResult> GetRates()
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://api.fixer.io/latest");
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CurrencyResult>(result);
        }
    }
}
