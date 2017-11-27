using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleSampleStockAppServices.Data
{
    public static class DbInitializer
    {
        public static void SeedDatabase(SimpleStockAppDataContext dataContext)
        {
            System.Diagnostics.Debug.WriteLine("Start Creating Dow");
            var dow =
            new StockIndex()
            {
                Name = "Dow Jones Industrial Average",
                Symbol = "^DJI"
            };
            dow.StockStockIndex = CreateStocksInDow(dow, dataContext);
            dataContext.StockIndexes.Add(dow);
            dataContext.SaveChanges();
            System.Diagnostics.Debug.WriteLine("Dow Done");

            System.Diagnostics.Debug.WriteLine("Start Creating Dax");
            var dax =
            new StockIndex()
            {
                Name = "DAX",
                Symbol = "^GDAXI"
            };
            dax.StockStockIndex = CreateStocksInDax(dax, dataContext);
            dataContext.StockIndexes.Add(dax);
            dataContext.SaveChanges();
            System.Diagnostics.Debug.WriteLine("Dax Done");

            System.Diagnostics.Debug.WriteLine("Adding quotes started");
            AddRealTimeData(dataContext);
            System.Diagnostics.Debug.WriteLine("Adding Done");
        }

        private static List<StockStockIndex> CreateStocksInDow(StockIndex stockIndex, SimpleStockAppDataContext dataContext)
        {
            var lst = new List<Stock>()
            {
                new Stock{ Symbol = "AAPL", Name = "Apple Inc.", Currency = "USD" },
                new Stock{ Symbol = "AXP", Name = "American Express Company", Currency = "USD" },
                new Stock{ Symbol = "BA", Name = "The Boeing Compan", Currency = "USD" },
                new Stock{ Symbol = "CSCO", Name = "Cisco systems", Currency = "USD" },
                new Stock{ Symbol = "GE", Name = "General Electric", Currency = "USD" },
                new Stock{ Symbol = "IBM", Name = "International Business Machines Corporation", Currency = "USD" },
                new Stock{ Symbol = "INTC", Name = "Intel Corporation", Currency = "USD" },
                new Stock{ Symbol = "JNJ", Name = "Johnson & Johnson", Currency = "USD" },
                new Stock{ Symbol = "JPM", Name = "JPMorgan Chase & Co.", Currency = "USD" },
                new Stock{ Symbol = "MMM", Name = "3M Company", Currency = "USD" },
                new Stock{ Symbol = "MSFT", Name = "Microsoft Corporation", Currency = "USD" },
                new Stock{ Symbol = "NKE", Name = "NIKE, Inc.", Currency = "USD" },
                new Stock{ Symbol = "V", Name = "Visa Inc", Currency = "USD" },
            };

            return AddStockListToIndex(lst, stockIndex, dataContext);
        }

        private static void AddRealTimeData(SimpleStockAppDataContext dataContext)
        {
            var realTimePirces = new List<TradingQuotes>
            {
                new TradingQuotes{ Symbol = "AAPL", Volume = 57191620,
                    LastValue = 172.50m,
                    DaysLow = 104.08m,
                    DaysHigh = 174.24m,
                    Change = 4.39m, ChangeInPercent = 2.52 },
                new TradingQuotes{ Symbol = "AXP",
                LastValue = 96.43m, Change = 0.45m, ChangeInPercent = 0.47,
                DaysHigh = 96.43m, DaysLow = 95.78m },
                new TradingQuotes{ Symbol = "BA",
                LastValue = 261.75m, Change = -0.88m, ChangeInPercent=-0.34,
                DaysLow = 260.08m, DaysHigh = 263.79m },
                new TradingQuotes{ Symbol = "CSCO",
                LastValue = 34.47m, Change = 0.26m, ChangeInPercent = 0.76,
                DaysLow = 34.03m, DaysHigh=34.49m },
                new TradingQuotes{ Symbol = "GE",  LastValue = 20.14m,
                Change = 0.2m, ChangeInPercent= 1, DaysLow = 19.86m, DaysHigh= 20.33m },
                new TradingQuotes{ Symbol = "IBM", LastValue =151.58m, Change =-1.77m, ChangeInPercent = -1.15 },
                new TradingQuotes{ Symbol = "INTC", LastValue =46.34m, Change= -0.76m, ChangeInPercent=-1.61 },
                new TradingQuotes{ Symbol = "JNJ", LastValue = 140.08m, Change=0.15m, ChangeInPercent=+0.11 },
                new TradingQuotes{ Symbol = "JPM", LastValue = 101.41m, Change = -0.18m, ChangeInPercent=-0.18 },
                new TradingQuotes{ Symbol = "MMM", LastValue=86.58m, Change=0.07m, ChangeInPercent=+0.08 },
                new TradingQuotes{ Symbol = "MSFT", LastValue=84.14m, Change=0.09m, ChangeInPercent=+0.11 },
                new TradingQuotes{ Symbol = "NKE", LastValue=55.71m, Change=0.59m, ChangeInPercent=+1.07 },
                new TradingQuotes{ Symbol ="ADS.DE", LastValue=187.00m, Change= -0.15m, ChangeInPercent=   -0.08 },
                new TradingQuotes{ Symbol ="ALV.DE", LastValue=202.85m, Change=  -1.05m, ChangeInPercent=   -0.51},
                new TradingQuotes{ Symbol ="BAS.DE", LastValue=97.01m, Change=   1.02m, ChangeInPercent=    +1.06},
                new TradingQuotes{ Symbol ="BAYN.DE", LastValue=116.00m, Change=  1.60m, ChangeInPercent=   +1.40},
                new TradingQuotes{ Symbol ="BEI.DE", LastValue=97.88m, Change=   0.83m, ChangeInPercent=    +0.86},
                new TradingQuotes{ Symbol ="BMW.DE", LastValue =89.57m, Change=  -0.30m, ChangeInPercent=   -0.33},
                new TradingQuotes{ Symbol ="CBK.DE", LastValue=11.785m, Change=  -0.210m, ChangeInPercent=  -1.751},
                new TradingQuotes{ Symbol ="CON.DE", LastValue=222.25m, Change=  0.10m, ChangeInPercent=    +0.05},
                new TradingQuotes{ Symbol ="DAI.DE", LastValue=73.25m, Change=   0.16m, ChangeInPercent=    +0.22},
                new TradingQuotes{ Symbol ="DB1.DE", LastValue=91.03m, Change=   1.08m, ChangeInPercent=    +1.20},
                new TradingQuotes{ Symbol ="DBK.DE", LastValue=14.51m, Change=   -0.12m, ChangeInPercent=   -0.85},
                new TradingQuotes{ Symbol ="DPW.DE", LastValue=40.215m, Change=  0.085m, ChangeInPercent=   +0.212},
                new TradingQuotes{ Symbol ="DTE.DE", LastValue=15.64m, Change=   0.19m, ChangeInPercent=    +1.23},
                new TradingQuotes{ Symbol ="FME.DE", LastValue =83.20m, Change=   0.75m, ChangeInPercent=    +0.91},
                new TradingQuotes{ Symbol ="FRE.DE", LastValue =69.77m, Change=  0.46m, ChangeInPercent=    +0.66},
                new TradingQuotes{ Symbol ="HEI.DE", LastValue =85.45m, Change=  -1.07m, ChangeInPercent=   -1.24},
                new TradingQuotes{ Symbol ="LHA.DE", LastValue=28.04m, Change=   -0.04m, ChangeInPercent=   -0.16},
                new TradingQuotes{ Symbol ="LIN.DE", LastValue=188.65m, Change=  0.95m, ChangeInPercent=    +0.51},
                new TradingQuotes{ Symbol ="SAP.DE", LastValue =98.85m, Change=   0.06m, ChangeInPercent=    +0.06},
                new TradingQuotes{ Symbol ="SIE.DE", LastValue=124.20m, Change=  -0.30m, ChangeInPercent=   -0.24},
                new TradingQuotes{ Symbol ="TKA.DE", LastValue=23.35m, Change=   -0.09m, ChangeInPercent=   -0.38}
            };

            dataContext.StockQuotes.AddRange(realTimePirces);
            dataContext.SaveChanges();
        }

        private static List<StockStockIndex> CreateStocksInDax(StockIndex index, SimpleStockAppDataContext dataContext)
        {
            var lst = new List<Stock>()
            {
              new Stock{ Symbol ="ADS.DE", Name="Addidas", Currency = "EUR" },
              new Stock{ Symbol ="ALV.DE", Name = "Allianz SE", Currency = "EUR" },
              new Stock{ Symbol ="BAS.DE", Name = "BASF SE", Currency = "EUR" },
              new Stock{ Symbol ="BAYN.DE", Name = "Bayer Aktiengesellschaft ", Currency = "EUR" },
              new Stock{ Symbol ="BEI.DE", Name = "Beiersdorf Aktiengesellschaft", Currency = "EUR" },
              new Stock{ Symbol ="BMW.DE", Name ="Bayerische Motoren Werke Aktiengesellschaft", Currency = "EUR" },
              new Stock{ Symbol ="CBK.DE", Name = "Commerzbank AG", Currency = "EUR" },
              new Stock{ Symbol ="CON.DE", Name = "Continental Aktiengesellschaft", Currency = "EUR" },
              new Stock{ Symbol ="DAI.DE", Name = "Daimler AG", Currency = "EUR" },
              new Stock{ Symbol ="DB1.DE", Name = "Deutsche Börse Aktiengesellschaft", Currency = "EUR" },
              new Stock{ Symbol ="DBK.DE", Name = "Deutsche Bank Aktiengesellschaft", Currency = "EUR" },
              new Stock{ Symbol ="DPW.DE", Name = "Deutsche Post AG", Currency = "EUR" },
              new Stock{ Symbol ="DTE.DE", Name = "Deutsche Telekom AG", Currency = "EUR" },
              new Stock{ Symbol ="FME.DE", Name = "Fresenius Medical Care AG", Currency = "EUR" },
              new Stock{ Symbol ="FRE.DE", Name = "Fresenius SE & Co.", Currency = "EUR" },
              new Stock{ Symbol ="HEI.DE", Name = "HeidelbergCement AG", Currency = "EUR" },
              new Stock{ Symbol ="LHA.DE", Name = "Lufthansa", Currency = "EUR" },
              new Stock{ Symbol ="LIN.DE", Name = "Linde", Currency = "EUR" },
              new Stock{ Symbol ="SAP.DE", Name = "SAP SE", Currency = "EUR" },
              new Stock{ Symbol ="SIE.DE", Name = "Siemens Aktiengesellschaft", Currency = "EUR" },
              new Stock{ Symbol ="TKA.DE", Name = "ThyssenKrupp AG", Currency = "EUR" }
            };

            return AddStockListToIndex(lst, index, dataContext);
        }

        private static List<StockStockIndex> AddStockListToIndex(List<Stock> Stocks, StockIndex Index, SimpleStockAppDataContext dataContext)
        {
            var retVal = new List<StockStockIndex>();

            foreach (var item in Stocks)
            {
                if (dataContext.Stocks.Where(n => n.Symbol == item.Symbol).Count() == 0)
                {
                    dataContext.Stocks.Add(item);
                }
                retVal.Add(new StockStockIndex { StockIndex = Index, Stock = item});
            }

            return retVal;
        }

    }
}
