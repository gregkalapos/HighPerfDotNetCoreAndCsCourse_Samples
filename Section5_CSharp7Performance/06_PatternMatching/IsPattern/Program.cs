using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace IsPattern
{
    class Person
    {
        public String Name { get; set; }
    }

    /// <summary>
    /// In this Demo we compare the IL code of:
    /// 1: C# "is - as" construct in pre C# 7
    /// 2: "is-Patterns" in C# 7 pattern matching 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Is-As Construct - Pre C# 7 style
        /// </summary>
        /// <param name="item"></param>
        public static void ProcessPerson_IsAs(Object item)
        {
            if (item is Person)
            {
                var person = item as Person;
                person.Name = "New Name";
            }
        }

        /// <summary>
        /// Pattern matching with C# 7
        /// </summary>
        /// <param name="item"></param>
        public static void ProcessPerson_Pattern(Object item)
        {
            if (item is Person p)
            {
                p.Name = "New Name";
            }
        }

        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Program>();
        }

        [Benchmark]
        public void ClassicIsAs1MioTimes()
        {
            Person p = new Person();
            p.Name = "Test";

            //500K matches
            for (int i = 0; i < 500_000; i++)
            {
                ProcessPerson_IsAs(p);
            }

            //500K non-matches
            for (int i = 0; i < 500_000; i++)
            {
                ProcessPerson_IsAs(42);
            }
        }
        
        [Benchmark]
        public void IsPattern1MioTimes()
        {
            Person p = new Person();
            p.Name = "Test";
            //500K matches
            for (int i = 0; i < 500_000; i++)
            {
                ProcessPerson_Pattern(p);
            }

            //500K non-matches
            for (int i = 0; i < 500_000; i++)
            {
                ProcessPerson_Pattern(42);
            }
        }
    }
}
