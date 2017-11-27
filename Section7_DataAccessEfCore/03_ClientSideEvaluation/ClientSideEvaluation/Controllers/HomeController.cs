using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClientSideEvaluation.Models;
using ClientSideEvaluation.Data;
using Microsoft.EntityFrameworkCore;

namespace ClientSideEvaluation.Controllers
{
    public class HomeController : Controller
    {
        public SampleDataContext SampleDataContext { get; }

        public HomeController(SampleDataContext sampleDataContext)
        {
            SampleDataContext = sampleDataContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        /// <summary>
        /// This request creates a query that uses client side execution.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public async Task<IActionResult> CommentsWithDotNetCore()
        {
            //this query fetches the whole Comments table into the ASP.NET Core application
            //from the database thanks to the ContainsDotNetCore in the where filter.
            var comments =  await SampleDataContext.Comments
                .Where(n => ContainsDotNetCore(n.Text)).Select(n => n.Text).ToListAsync();

            return View(comments);
        }

        /// <summary>
        /// Called within a Where filter.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static bool ContainsDotNetCore(string text)
        {
            return text.ToLower().Contains(".net core");
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
