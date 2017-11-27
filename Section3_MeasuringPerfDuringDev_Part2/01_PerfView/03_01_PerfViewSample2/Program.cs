using System;
using System.Collections.Generic;

namespace _03_01_PerfViewSample2
{
    /// <summary>
    /// Sample application for the 2. Demo in Video 3.1.
    /// We use ETW and PerfView to check the GC round and GC roots within this application.
    /// </summary>
    class Program
    {
        public static List<Person> People = new List<Person>();

        static void Main(string[] args)
        {
            for (int i = 0; i < 1000; i++)
            {
                CreatePerson();
            }

            Console.WriteLine("Done");
        }

        public static void CreatePerson()
        {
            int[] newArray = new int[100000 * 1024]; //allocate large object
            People.Add(new Person { Name = "Test Person", Age = 34 });
        }
    }

    public class Person
    {
        public String Name { get; set; }
        public int Age { get; set; }
    }
}


