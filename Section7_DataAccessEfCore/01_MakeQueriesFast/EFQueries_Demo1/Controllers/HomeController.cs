using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EFQueries_Demo1.Models;
using EFQueries_Demo1.Data;
using Microsoft.EntityFrameworkCore;

namespace EFQueries_Demo1.Controllers
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

        #region VERSION1
        //Problem: this fethes the whole table
        //Solution: don't do filtering in the .NET Core app, do it in the database
        //public IActionResult UsersBornBefore1980()
        //{
        //    List<String> users = new List<string>();

        //    var d1980 = new DateTime(1980, 1, 1);
        //    var dbUsers = SampleDataContext.Users;

        //    foreach (var item in dbUsers)
        //    {
        //        if(item.BirthDate < d1980)
        //        {
        //            users.Add(item.UserName);
        //        }
        //    }

        //    return View(users);
        //}
        #endregion

        #region VERSION2
        //Problem: This loads all the columns, altough we only use the username
        //Solution: use the select method to only select username
        //public IActionResult UsersBornBefore1980() 
        //{
        //	List<String> users = new List<string>();

        //	var d1980 = new DateTime(1980, 1, 1);
        //	var dbUsers = SampleDataContext.Users.Where(n => n.BirthDate < d1980);

        //	foreach (var item in dbUsers) 
        //	{				
        //			users.Add(item.UserName);				
        //	}

        //	return View(users);
        //}
        #endregion

        #region VERSION3
        //Problem: There is still no limit on the number of rows we return. 
        //If the where matches on 9 million rows we fetch 9 million rows from the Db
        //Solution: Use the Skip and the Take methods to limit the number of rows we return
        //public IActionResult UsersBornBefore1980() 
        //{
        //	List<String> users = new List<string>();

        //	var d1980 = new DateTime(1980, 1, 1);
        //	users = SampleDataContext.Users.Where(n => n.BirthDate < d1980).Select(n => n.UserName).ToList();

        //	return View(users);
        //}
        #endregion


        #region VERSION4
        //Problem: This method blocks an ASP.NET Core threadpool thread until the database works
        //Solution: Use EF Core async query methods
        //public IActionResult UsersBornBefore1980([FromQuery]int pageNumber) 
        //{
        //	List<String> users = new List<string>();

        //	var d1980 = new DateTime(1980, 1, 1);
        //	users = SampleDataContext.Users.Where(n => n.BirthDate < d1980).Select(n => n.UserName).Skip(pageNumber *10).Take(10).ToList();

        //	return View(users);
        //}
        #endregion

        #region VERSION5        
        public async Task<IActionResult> UsersBornBefore1980([FromQuery]int pageNumber)
        {
			List<String> users = new List<string>();

			var d1980 = new DateTime(1980, 1, 1);
			users = await SampleDataContext.Users.Where(n => n.BirthDate < d1980).Select(n => n.UserName).Skip(pageNumber * 10).Take(10).ToListAsync();
            
			return View(users);
		}
        #endregion

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
