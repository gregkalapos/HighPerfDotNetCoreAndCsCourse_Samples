using System;
using System.Collections.Generic;
using System.Diagnostics;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Demo4_ListVsHashSet_BadHashCode
{
	/// <summary>
	/// This sample is the same as Demo3_ListVsHashSet_Lookup, but in this case the HashSet has a very inefficient GetHashCode method
	/// </summary>
	public class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Program>();
        }

        struct MyNumber : IEquatable<MyNumber>
        {
            public int Number;

            public MyNumber(int N)
            {
                Number = N;
            }

            public override bool Equals(object obj)
            {
                return obj is MyNumber && Equals((MyNumber)obj);
            }

            public bool Equals(MyNumber other)
            {
                return Number == other.Number;
            }

            /// <summary>
            /// PERFORMANCE KILLER GetHashCode implementation
            /// </summary>
            /// <returns></returns>
            public override int GetHashCode()
            {
                return 42;
            }

            public static bool operator ==(MyNumber number1, MyNumber number2)
            {
                return number1.Equals(number2);
            }

            public static bool operator !=(MyNumber number1, MyNumber number2)
            {
                return !(number1 == number2);
            }
        }

        static HashSet<MyNumber> HashSet = new HashSet<MyNumber>();
        static List<MyNumber> List = new List<MyNumber>();


        static Program()
        {
            for (int i = 0; i < 100_000; i++)
            {
                List.Add(new MyNumber(i));
                HashSet.Add(new MyNumber(i));
            }
        }


        [Benchmark]
        public static void LookupInLinkedList()
        {
            var result = List.Contains(new MyNumber(-1));
            Debug.WriteLine(result);
        }

        [Benchmark]
        public static void LookupInHashSet()
        {
            var result = HashSet.Contains(new MyNumber(-1));
            Debug.WriteLine(result);
        }


    }
}
