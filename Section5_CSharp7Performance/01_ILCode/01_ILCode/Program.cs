using System;

namespace _01_ILCode
{
    /// <summary>
    /// This small application serves as a sample for opening IL code.
    /// In Section 5.01 we compile this code and open it in dnSpy and see how the IL code looks.
    /// </summary>
    class Program
    {
        public static int Add(int a, int b)
        {
            return a + b;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
