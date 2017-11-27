using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace TupleSample
{
    /// <summary>
    /// Sample application to compare the performance of System.Tuple and System.ValueTuple
    /// </summary>
    [MemoryDiagnoser]
    public class Program
    {
        public static Tuple<int, int> GetMinMax_SystemTuple(List<int> values)
        {
            int min = values[0];
            int max = values[0];
            foreach (var item in values)
            {
                if (item < min)
                    min = item;
                if (item > max)
                    max = item;
            }
            return Tuple.Create<int, int>(min, max);
        }

        public static (int min, int max) GetMinMax_CS7Tuple(List<int> values)
        {
            int min = values[0];
            int max = values[0];
            foreach (var item in values)
            {
                if (item < min)
                    min = item;
                if (item > max)
                    max = item;
            }
            return (min, max);
        }
        
                
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Program>();
        }

        [Benchmark]
        public void TestValueTuple()
        {
            var numbers = new List<int> { 3, 432, 21, 32, 43, 45, 1 };

            for (int i = 0; i < 1_000_000; i++)
            {
                var v = GetMinMax_CS7Tuple(numbers);
                Debug.WriteLine($"Min: {v.min}, Max: {v.max}");
            }
        }

        [Benchmark]
        public void TestSystemTuple()
        {
            var numbers = new List<int> { 3, 432, 21, 32, 43, 45, 1 };

            for (int i = 0; i < 1_000_000; i++)
            {
                var v = GetMinMax_SystemTuple(numbers);
                Debug.WriteLine($"Min: {v.Item1}, Max: {v.Item2}");
            }
        }
    }
}
