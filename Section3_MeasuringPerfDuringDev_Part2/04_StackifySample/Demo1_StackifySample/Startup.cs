using System;
using System.Linq;
using Demo1_StackifySample.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Demo1_StackifySample
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MyTestDbContext>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //Add Stackify middleware
            app.UseMiddleware<StackifyMiddleware.RequestTracerMiddleware>();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            var dbContext = app.ApplicationServices.GetService(typeof(MyTestDbContext)) as MyTestDbContext;
            dbContext.Database.EnsureCreated();
            dbContext.Database.Migrate();
            PopulateDb(dbContext);

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
