using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Demo1_StackifySample.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Demo1_StackifySample.Model;

namespace Demo1_StackifySample.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyTestDbContext myTestDbContext;

        public HomeController(MyTestDbContext myTestDbContext)
        {
            this.myTestDbContext = myTestDbContext;
        }

        /// <summary>
        /// Returns the portfolio from the Database an converts every currency to eur. 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var ratesTask = GetRates();
            var portfolio = myTestDbContext.UserPortfolioItems.ToList();
            var portfolioInEur = new List<ValueInEur>();

            var rates = await ratesTask;

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

            return View(portfolioInEur);
        }

        /// <summary>
        /// Do an HTTP call to get the latest currency rates and deserialize it with json.net
        /// </summary>
        /// <returns></returns>
        public async Task<CurrencyResult> GetRates()
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://api.fixer.io/latest");
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CurrencyResult>(result);
        }
    }
}
