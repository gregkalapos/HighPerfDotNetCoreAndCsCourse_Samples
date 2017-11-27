using System;
using System.Diagnostics;

namespace Demo1_NoPooling
{
    /// <summary>
    /// This small program calls a method 1000 times that allocates a big
    /// array on the LOH. We compare this method to a similar method that relies on
    /// the ArrayPool instead of manual array allocation
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 1000 * 10; i++)
            {
                MethodCalledOften();
            }
        }

        public static void MethodCalledOften()
        {
            var arr = new int[256 * 1024];
            Debug.WriteLine(arr[0].ToString());
            //use arr.
        }
    }
}
