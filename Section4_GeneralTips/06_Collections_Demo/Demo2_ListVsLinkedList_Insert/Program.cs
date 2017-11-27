using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Demo2_ListVsLinkedList_Insert
{
	/// <summary>
	/// We insert 25_000 items to the first position within a
	/// 1) List<int>
	/// 2) and a LinkedList<int>
	/// And we compare the performance of these two.
	/// </summary>
	public class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Program>();
        }

        [Benchmark]
        public static void InsertFirstList()
        {
            var list = new List<int>();
            Random rnd = new Random();

            for (int i = 0; i < 25_000; i++)
            {
                list.Insert(0, rnd.Next());
            }
        }

        [Benchmark]
        public static void InsertFirstLinkedList()
        {
            var linkedList = new LinkedList<int>();
            Random rnd = new Random();

            for (int i = 0; i < 25_000; i++)
            {
                linkedList.AddFirst(rnd.Next());
            }
        }
    }
}
