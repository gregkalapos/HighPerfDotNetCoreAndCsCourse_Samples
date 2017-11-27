using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace ValueTaskSample
{
    /// <summary>
    /// This sample app compares the performance of Task and ValueTask
    /// </summary>
    [MemoryDiagnoser]
    public class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Program>();
        }

        [Benchmark]
        public async Task ValueTaskImpl()
        {
            var date = new DateTime(1995, 01, 01);
            var values = await GetHistoricalData(date);
            var lastyear = new DateTime(2017, 01, 01);

            while (date != lastyear)
            {
                date = date.AddDays(1);
                values = await GetHistoricalData(date);
                Debug.WriteLine(values.Last().Date);
            }
        }

        [Benchmark]
        public async Task ClassicTaskImpl()
        {
            var date = new DateTime(1995, 01, 01);
            var values = await GetHistoricalData_TaskClassic(date);
            var lastyear = new DateTime(2017, 01, 01);

            while (date != lastyear)
            {
                date = date.AddDays(1);
                values = await GetHistoricalData_TaskClassic(date);
                Debug.WriteLine(values.Last().Date);
            }
        }
        
        /// <summary>
        /// Returns a Task + uses a cache
        /// </summary>
        /// <param name="StartDate"></param>
        /// <returns></returns>
        public static Task<IEnumerable<HistoricalQuote>> GetHistoricalData_TaskClassic(DateTime StartDate)
        {
            if (HistoricalDataCache_ForTaskClassic?.Last().Date < StartDate)
                return Task.FromResult(HistoricalDataCache_ForTaskClassic.Where(n => n.Date >= StartDate));
            else
            {
                var task = HistoricalPriceReader.GetNewerThan(StartDate);
                task.ContinueWith((n) => HistoricalDataCache_ForTaskClassic = n.Result);
                return task;
            }
        }

        /// <summary>
        /// Returns a ValueTask + uses a cache
        /// </summary>
        /// <param name="StartDate"></param>
        /// <returns></returns>
        public static ValueTask<IEnumerable<HistoricalQuote>> GetHistoricalData(DateTime StartDate)
        {
            if (HistoricalDataCache?.Last().Date < StartDate)
                return new ValueTask<IEnumerable<HistoricalQuote>>(HistoricalDataCache.Where(n => n.Date >= StartDate));
            else
            {
                var task = HistoricalPriceReader.GetNewerThan(StartDate);
                task.ContinueWith((n) => HistoricalDataCache = n.Result);
                return new ValueTask<IEnumerable<HistoricalQuote>>(task);
            }
        }

        /// <summary>
        /// Cache used in the GetHistoricalData method
        /// </summary>
        public static IEnumerable<HistoricalQuote> HistoricalDataCache;

        /// <summary>
        /// Cache used in the GetHistoricalData_TaskClassic method
        /// </summary>
        public static IEnumerable<HistoricalQuote> HistoricalDataCache_ForTaskClassic;
    }

    public static class HistoricalPriceReader
    {
        /// <summary>
        /// Reads the historical data of the MSFT stock from the MSFT.csv file
        /// </summary>
        /// <param name="FromDate"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<HistoricalQuote>> GetNewerThan(DateTime FromDate)
        {
            var retVal = new List<HistoricalQuote>();
            var fileName = "MSFT.csv";
            var file = System.IO.File.OpenRead(fileName);
            using (var logReader = new System.IO.StreamReader(file))
            {

                string line;
                while ((line = await logReader.ReadLineAsync()) != null)
                {
                    DateTime currentDate;
                    string[] items;
                    try
                    {
                        items = line.Split(';');
                        var date = items[0].Split('-');
                        currentDate = new DateTime(Int32.Parse(date[0]), Int32.Parse(date[1]), Int32.Parse(date[2]));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Reading csv failed: {e.Message}");
                        break;
                    }
                    if (currentDate < FromDate)
                    {
                        break;
                    }

                    retVal.Add(
                       new HistoricalQuote
                       {
                           Date = currentDate,
                           Close = Decimal.Parse(items[1].Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture),
                           High = Decimal.Parse(items[2].Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture),
                           Low = Decimal.Parse(items[3].Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture),
                           Open = Decimal.Parse(items[4].Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture),
                           Volume = long.Parse(items[5].Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture)
                       });
                }
            }

            return retVal;
        }
    }

    public class HistoricalQuote
    {

        public DateTime Date { get; set; }

        public Decimal Open { get; set; }

        public Decimal High { get; set; }

        public Decimal Low { get; set; }

        private decimal close;
        public Decimal Close
        {
            get { return Math.Round(close, 2); }
            set { close = value; }
        }

        public long Volume { get; set; }

        public String Symbol { get; set; }
    }
}
