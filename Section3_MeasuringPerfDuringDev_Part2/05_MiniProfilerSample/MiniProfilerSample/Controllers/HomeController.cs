using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniProfilerSample.Data;
using MiniProfilerSample.Model;
using MiniProfilerSample.Models;
using Newtonsoft.Json;
using StackExchange.Profiling;

namespace MiniProfilerSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyTestDbContext myTestDbContext;

        public HomeController(MyTestDbContext myTestDbContext)
        {
            this.myTestDbContext = myTestDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var rates = await GetRates();
            var portfolio = myTestDbContext.UserPortfolioItems.ToList();
            var portfolioInEur = new List<ValueInEur>();

            using (MiniProfiler.Current.Step("Convert currencies"))
            {
                foreach (var item in portfolio)
                {
                    switch (item.CurrencyName)
                    {
                        case nameof(Rates.CAD):
                            portfolioInEur.Add(new ValueInEur { CurrencyName = nameof(Rates.CAD), ValueInOrigCurrency = item.Amount, ValueEur = item.Amount / (Decimal)rates.Rates.CAD });
                            break;
                        case nameof(Rates.USD):
                            portfolioInEur.Add(new ValueInEur { CurrencyName = nameof(Rates.USD), ValueInOrigCurrency = item.Amount, ValueEur = item.Amount / (Decimal)rates.Rates.USD });
                            break;
                        case nameof(Rates.HUF):
                            portfolioInEur.Add(new ValueInEur { CurrencyName = nameof(Rates.HUF), ValueInOrigCurrency = item.Amount, ValueEur = item.Amount / (Decimal)rates.Rates.HUF });
                            break;
                        case nameof(Rates.NZD):
                            portfolioInEur.Add(new ValueInEur { CurrencyName = nameof(Rates.NZD), ValueInOrigCurrency = item.Amount, ValueEur = item.Amount / (Decimal)rates.Rates.NZD });
                            break;
                        default:
                            break;
                    }
                }
            }
            return View(portfolioInEur);
        }

        /// <summary>
        /// Do an HTTP call to get the latest currency rates and deserializes it with json.net
        /// </summary>
        /// <returns></returns>
        public async Task<CurrencyResult> GetRates()
        {
            var url = "http://api.fixer.io/latest";
            string result = String.Empty;
            using (MiniProfiler.Current.CustomTiming("http", "GET " + url))
            {
                HttpClient httpClient = new HttpClient();
                var response = await httpClient.GetAsync(url);
                result = await response.Content.ReadAsStringAsync();
            }

            return JsonConvert.DeserializeObject<CurrencyResult>(result);
        }
    }
}
