using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniProfilerSample.Data;

namespace MiniProfilerSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            MyTestDbContext myTestDbContext = new MyTestDbContext();
            myTestDbContext.Database.EnsureCreated();
            myTestDbContext.Database.Migrate();
            PopulateDb(myTestDbContext);

            services.AddDbContext<MyTestDbContext>();
            services.AddMiniProfiler().AddEntityFramework(); //Add EFCore tracking to MiniProfiler

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiniProfiler(); //Add the MiniProfiler middleware

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }


        /// <summary>
        /// Populates the database with some entries if it is empty
        /// </summary>
        private void PopulateDb(MyTestDbContext myTestDbContext)
        {
            if (myTestDbContext.UserPortfolioItems.Count() == 0)
            {
                Random rnd = new Random();
                myTestDbContext.UserPortfolioItems.Add(new UserPortfolioItem() { Amount = (Decimal)rnd.NextDouble() * rnd.Next(50_000), CurrencyName = nameof(Rates.CAD) });
                myTestDbContext.UserPortfolioItems.Add(new UserPortfolioItem() { Amount = (Decimal)rnd.NextDouble() * rnd.Next(50_000), CurrencyName = nameof(Rates.USD) });
                myTestDbContext.UserPortfolioItems.Add(new UserPortfolioItem() { Amount = (Decimal)rnd.NextDouble() * rnd.Next(50_000), CurrencyName = nameof(Rates.HUF) });
                myTestDbContext.UserPortfolioItems.Add(new UserPortfolioItem() { Amount = (Decimal)rnd.NextDouble() * rnd.Next(50_000), CurrencyName = nameof(Rates.NZD) });

                myTestDbContext.SaveChanges();
            }
        }
    }
}
