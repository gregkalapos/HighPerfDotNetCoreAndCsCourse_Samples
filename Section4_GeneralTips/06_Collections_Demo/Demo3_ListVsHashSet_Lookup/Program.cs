using System;
using System.Collections.Generic;
using System.Diagnostics;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Demo3_ListVsHashSet_Lookup 
{
	/// <summary>
	/// In this small app we have a HashSet and a List, each with 1_000_000 items. We compare the lookup performance of these two collections.
	/// In the two benchmark method we search for an item that is not contained in the collection, once with the HashSet and once with the List.
	/// </summary>
    public class Program
    {
        static HashSet<int> HashSet = new HashSet<int>();
        static List<int> List = new List<int>();


        static Program()
        {
            for (int i = 0; i < 1_000_000; i++)
            {
                List.Add(i);
                HashSet.Add(i);
            }
        }


        [Benchmark]
        public static void LookupInLinkedList()
        {
            var result = List.Contains(-1);
            Debug.WriteLine(result);
        }

        [Benchmark]
        public static void LookupInHashSet()
        {
            var result = HashSet.Contains(-1);
            Debug.WriteLine(result);
        }


        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Program>();
        }
    }
}
