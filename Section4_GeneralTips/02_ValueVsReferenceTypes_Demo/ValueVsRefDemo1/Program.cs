using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ValueVsRefDemo1
{
    /// <summary>
    /// A simple reference type
    /// </summary>
    public class Point_Class
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    /// <summary>
    /// A value type without proper equals methods
    /// </summary>
    public struct Point_Struct_NoOverLoad
    {
        public int x;
        public int y;
    }

    /// <summary>
    /// A value type that properly imlpements the equals method to avoid boxin and reflection
    /// </summary>
    public struct Point_Struct_ProperImplementation : IEquatable<Point_Struct_ProperImplementation>
    {
        public int x;
        public int y;

        public override bool Equals(object obj)
        {
            return obj is Point_Struct_ProperImplementation && Equals((Point_Struct_ProperImplementation)obj);
        }

        public bool Equals(Point_Struct_ProperImplementation other)
        {
            return x == other.x &&
                   y == other.y;
        }

        public override int GetHashCode()
        {
            var hashCode = 1502939027;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Point_Struct_ProperImplementation point1, Point_Struct_ProperImplementation point2)
        {
            return point1.Equals(point2);
        }

        public static bool operator !=(Point_Struct_ProperImplementation point1, Point_Struct_ProperImplementation point2)
        {
            return !(point1 == point2);
        }
    }


    /// <summary>
    /// Sample application to show the performance difference between value types and reference types:
    /// We have 3 different types (1 reference type, 1 value type without proper equals method and 1 value type with proper equal method
    /// In the benchmarkdotnet test methods we create a list from these type, add 1mio items to the lists and search for an item that is not in the list
    /// </summary>
    [MemoryDiagnoser]
    public class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Program>();
        }

        /// <summary>
        /// 
        /// </summary>
        [Benchmark]
        public static void ManyClasses()
        {
            Random rnd = new Random();
            var list = new List<Point_Class>();

            for (int i = 0; i < 1_000_000; i++)
            {
                list.Add(new Point_Class() { X = rnd.Next(), Y = rnd.Next() });
            }

            list.Contains(new Point_Class { X = -1, Y = -1 }); //won't be in the the list, since this is a new reference
        }

        [Benchmark]
        public static void ManyStructs_NoOverLoad()
        {
            Random rnd = new Random();
            var list = new List<Point_Struct_NoOverLoad>();

            for (int i = 0; i < 1_000_000; i++)
            {
                list.Add(new Point_Struct_NoOverLoad() { x = rnd.Next(), y = rnd.Next() });
            }

            list.Contains(new Point_Struct_NoOverLoad { x = -1, y = -1 }); //won't be in the list, since rnd.Next() only returns non-negative numbers
        }

        [Benchmark]
        public static void ManyStructs_ProperImplementation()
        {
            Random rnd = new Random();
            var list = new List<Point_Struct_ProperImplementation>();

            for (int i = 0; i < 1_000_000; i++)
            {
                list.Add(new Point_Struct_ProperImplementation() { x = rnd.Next(), y = rnd.Next() });
            }

            list.Contains(new Point_Struct_ProperImplementation { x = -1, y = -1 }); //won't be in the list, since rnd.Next() only returns non-negative numbers
        }
    }
}