using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EFCore_DepenentEntities_Demo2.Models;
using EFCore_DepenentEntities_Demo2.Data;
using Microsoft.EntityFrameworkCore;

namespace EFCore_DepenentEntities_Demo2.Controllers
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
        //Problem: N+1 query problem. We query 10 users (1 query returning 10 items) and then do 10 queries to retrieve the comments 
        //in sum 10+1=11 queries 
        //Solution: use features from EF Core that help us to load related entities, in this case the .Include method for eager loading
        //public async Task<IActionResult> UsersBornBefore1980WithComments([FromQuery]int pageNumber)
        //{
        //    List<UserWithComments> usersWithComments = new List<UserWithComments>();

        //    var d1980 = new DateTime(1980, 1, 1);
        //    var users = await SampleDataContext.Users.Where(n => n.BirthDate < d1980).Select(n => n.UserName).Skip(pageNumber * 10).Take(10).ToListAsync();

        //    foreach (var item in users)
        //    {
        //        var comments = SampleDataContext.Comments.Where(n => n.Author.UserName == item).Select(n => n.Text).ToList();
        //        usersWithComments.Add(new UserWithComments { UserName = item, Comments = comments });
        //    }

        //    return View(usersWithComments);
        //}
        #endregion

        #region VERSION2
        //Problem: This version throws a NullReferenceException, since we don't tell EF Core to load related entities and
        //lazy loading is not supported in EF Core (current version: 2.0)
        //Solution: Use eager loading
        //public async Task<IActionResult> UsersBornBefore1980WithComments([FromQuery]int pageNumber)
        //{
        //    List<UserWithComments> usersWithComments = new List<UserWithComments>();

        //    var d1980 = new DateTime(1980, 1, 1);
        //    var users = await SampleDataContext.Users.Where(n => n.BirthDate < d1980).Skip(pageNumber * 10).Take(10).ToListAsync();

        //    foreach (var item in users)
        //    {
        //        var comments = item.Comments.Select(n => n.Text).ToList();
        //        usersWithComments.Add(new UserWithComments
        //        {
        //            UserName = item.UserName,
        //            Comments = comments
        //        });
        //    }

        //    return View(usersWithComments);
        //}
        #endregion

        #region VERSION3
        //Problem: This query loads all the columns from both from the users and the comments table, altough we only use
        //the UserName and the Text columns
        //Solution: Use the SelectMany method
        //public async Task<IActionResult> UsersBornBefore1980WithComments([FromQuery]int pageNumber)
        //{
        //    List<UserWithComments> usersWithComments = new List<UserWithComments>();

        //    var d1980 = new DateTime(1980, 1, 1);
        //    var users = await SampleDataContext.Users.Include(n => n.Comments)
        //        .Where(n => n.BirthDate < d1980)
        //        .Skip(pageNumber * 10).Take(10).ToListAsync();

        //    foreach (var item in users)
        //    {
        //        var comments = item.Comments.Select(n => n.Text).ToList();
        //        usersWithComments.Add(new UserWithComments
        //        {
        //            UserName = item.UserName,
        //            Comments = comments
        //        });
        //    }

        //    return View(usersWithComments);
        //}
        #endregion

        #region VERSION4
        //Problem: no problem, this query only fetches the 2 columns we want. It loads the dependent entities with eager loading
        //In this sample we want to load the queries for all 10 users that we load
        //public async Task<IActionResult> UsersBornBefore1980WithComments([FromQuery]int pageNumber)
        //{
        //    List<UserWithComments> usersWithComments = new List<UserWithComments>();

        //    var d1980 = new DateTime(1980, 1, 1);
        //    var users = await SampleDataContext.Users
        //        .Include(n => n.Comments)
        //        .Where(n => n.BirthDate < d1980)
        //        .Skip(pageNumber * 10).Take(10)
        //        .SelectMany(n => n.Comments)
        //        .Select(n => new { n.Author.UserName, n.Text })
        //        .GroupBy(n => n.UserName).ToListAsync();

        //    foreach (var item in users)
        //    {
        //        usersWithComments.Add(new UserWithComments
        //        {
        //            UserName = item.Key,
        //            Comments = item.Select(n => n.Text).ToList()
        //        });
        //    }
        //    return View(usersWithComments);
        //}
        #endregion


        //Version5: In this version we specifically load the comments only for the first user.
        //We use explicit loading here with the .Entry, .Collection, and .Load methods
        public async Task<IActionResult> UsersBornBefore1980WithComments([FromQuery]int pageNumber)
        {
            List<UserWithComments> usersWithComments = new List<UserWithComments>();

            var d1980 = new DateTime(1980, 1, 1);
            var users = await SampleDataContext.Users.Where(n => n.BirthDate < d1980)
                .Skip(pageNumber * 10).Take(10).ToListAsync(); ;

            SampleDataContext.Entry(users.First()).Collection(n => n.Comments).Load();

            usersWithComments.Add(new UserWithComments
            {
                UserName = users.First().UserName,
                Comments = users.First().Comments.Select(n => n.Text).ToList()
            });

            for (int i = 1; i < 10; i++)
            {
                usersWithComments.Add(new UserWithComments { UserName = users.ElementAt(i).UserName });
            }

            return View(usersWithComments);
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
