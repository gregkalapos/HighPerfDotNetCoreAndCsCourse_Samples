using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace _02_03_Demo1_PerfTips
{
    /// <summary>
    /// This small program demonstrates how to use BenchmarkDotNet. 
    /// </summary>
    public class Program
    {

        static IEnumerable<HistoricalQuote> HistoricalData;

        static Program()
        {
            HistoricalPriceReader hpr = new HistoricalPriceReader();
            HistoricalData = hpr.GetHistoricalQuotes("MSFT");
        }
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Program>();

        }

        /// <summary>
        /// This benchmark method calls our original CalculateRsi method, which has a performance problem (Discussed in the Visual Studio Performance tools video)
        /// </summary>
        [Benchmark]
        public static void CalculateRsiOriginal()
        {
            var data = Rsi.CalculateRsi(HistoricalData, 14, CancellationToken.None);
            Debug.WriteLine(data.ToList().Count);
        }

        /// <summary>
        /// This benchmark method calls the CalculateRsiFixed() which contains a fix to the performance issue discussed in the Visual Studio Performance tools video
        /// </summary>
        [Benchmark]
        public static void CalculateRsiFixed()
        {
            var dataFromFixed = Rsi.CalculateRsiFixed(HistoricalData, 14, CancellationToken.None);
            Debug.WriteLine(dataFromFixed.ToList().Count);
        }

    }
}
