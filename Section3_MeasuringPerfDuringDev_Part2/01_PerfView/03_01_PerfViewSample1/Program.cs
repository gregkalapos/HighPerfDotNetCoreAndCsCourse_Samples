using System;

namespace _03_01_Demo1_PerfViewCpuSampling
{
    /// <summary>
    /// Small demo application to show you how to use PerfView. 
    /// In Video 3.1 we use this application to create a CPU Sample with PerfView
    /// </summary>
    class Program
    {
        static Random RND = new Random();
        static void Main(string[] args)
        {
            long sum = 0;
            for (int i = 0; i < 10000000; i++)
            {
                var a = RND.Next();
                var b = RND.Next();

                sum += Math.DivRem(a, b, out int result);
            }

            Console.WriteLine($"Done, sum: {sum}");
        }
    }
}
