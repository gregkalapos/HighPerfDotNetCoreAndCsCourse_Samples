using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Demo5_SortedSetVsHashSet_SortedItems
{
	/// <summary>
	/// This small app inserts 10_000 items into a SortedSet and a HashSet then prints a list of all items in order.
	/// We compare the performance of this operation with the two different data structures
	/// </summary>
	public class Program
    {
        static SortedSet<int> SortedSet = new SortedSet<int>();
        static HashSet<int> HashSet = new HashSet<int>();

    
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Program>();
        }

        [Benchmark]
        public static void PrintSortedItemsSortedSet()
        {
            Random rnd = new Random();

            while(SortedSet.Count < 10_000)
            {
                var n = rnd.Next(0, Int32.MaxValue);
                if (!SortedSet.Contains(n))
                {
                    SortedSet.Add(n);
                }

                //print 
                foreach (var item in SortedSet)
                {
                    Debug.WriteLine(item);
                }
            }
        }

        [Benchmark]
        public static void PrintSortedItemsHashSet()
        {
            Random rnd = new Random();

            while (HashSet.Count < 10_000)
            {
                var n = rnd.Next(0, Int32.MaxValue);
                if (!HashSet.Contains(n))
                {
                    HashSet.Add(n);
                }

                //extra sorting
                var keys = HashSet.ToList();
                keys.Sort();    
                
                //print
                foreach (var item in keys)
                {
                    Debug.WriteLine(item);
                }
            }
        }
    }
}
