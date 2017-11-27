using System;
using System.Buffers;
using System.Diagnostics;

namespace Demo2_ArrayPool
{
    /// <summary>
    /// A sample application that calls a method 1000 times 
    /// which uses the ArrayPool. In Video 6.1 we compare this method 
    /// to a similar method from Project Demo1_NoPooling that uses manual array allocation
    /// instead of pooling arrays with  ArrayPool
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 1000; i++)
            {
                MethodCalledOften();
            }
        }

        public static void MethodCalledOften()
        {
            var arr = ArrayPool<int>.Shared.Rent(256 * 1024);
            Debug.WriteLine(arr[0].ToString());
            //use arr.
            ArrayPool<int>.Shared.Return(arr);
        }
    }
}
