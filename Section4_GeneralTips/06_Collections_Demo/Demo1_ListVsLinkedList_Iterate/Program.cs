using System;
using System.Linq;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Demo1_ListVsLinkedList_Iterate
{
	/// <summary>
	/// This small program demonstrates the performance difference between
	/// iterating over a big LinkedList vs. iterating over a bug List
	/// </summary>
    public class Program
    {
        public static List<int> List = new List<int>();
        public static LinkedList<int> LinkedList = new LinkedList<int>();

        static Program()
        {
            Random rnd = new Random();
            for (int i = 0; i < 1_000_000; i++)
            {
                var n = rnd.Next();
                List.Add(n);
                LinkedList.AddLast(n);
            }
        }

        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Program>();
        }

        [Benchmark]
        public static void CheckForNonExistingItemInList()
        {
            var v = List.Contains(-1);
        }

        [Benchmark]
        public static void CheckForNonExistingItemInLinkedList()
        {
            var v = LinkedList.Contains(-1);
        }
    }
}
