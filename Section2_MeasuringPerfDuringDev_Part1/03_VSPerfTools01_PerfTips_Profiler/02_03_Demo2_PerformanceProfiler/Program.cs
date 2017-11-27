using System;
using System.Collections.Generic;

namespace _02_03_Demo3_PerformanceProfiler
{
    class Program
    {
        public static List<Person> People = new List<Person>();

        static void Main(string[] args)
        {
            for (int i = 0; i < 10000000; i++)
            {
                CreatePerson();
            }

            Console.WriteLine("Done");
            Console.ReadKey();
        }

        public static void CreatePerson()
        {
            People.Add(new Person { Name = "Test Person", Age = 34 });
        }
    }
    
    public class Person
    {
        public String Name { get; set; }
        public int Age { get; set; }
    }
}
