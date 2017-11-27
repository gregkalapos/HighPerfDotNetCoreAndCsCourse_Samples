using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using LocalFunctionPerformance_SampleApp;

namespace LocalFunctionsPerformanceSample
{
    /// <summary>
    /// This program calculates the Average True Range of the Microsoft stock with 10 different parameters.
    /// It contains two implementations: one uses Func<T>, another one uses a local function
    /// In Section 5_2 we compare the performance and discuss the reason for the difference.
    /// </summary>
    [MemoryDiagnoser]
    public class Program
    {
        /// <summary>
        /// Contains the historical data of MSFT. 
        /// </summary>
        static HistoricalValue[] historicalData = HistoricalDataStore.GetMsftHistoricalData();

        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Program>();
        }

        [Benchmark]
        public void CalculateAtr_Delegate()
        {
            for (int i = 5; i <= 15; i++)
            {
                var atrs = AverageTrueRange_Delegate.CalculateAverageTrueRange_Delegate(historicalData, i);
                Debug.WriteLine($"Number of ATR values: {atrs.Count}");
            }
        }

        [Benchmark]
        public void CalculateAtr_LocalFunction()
        {
            for (int i = 5; i <= 15; i++)
            {
                var atrs = AverageTrueRange_LocalFunction.CalculateAverageTrueRange_LocalFunction(historicalData, i);
                Debug.WriteLine($"Number of ATR values: {atrs.Count}");
            }
        }
    }
}

