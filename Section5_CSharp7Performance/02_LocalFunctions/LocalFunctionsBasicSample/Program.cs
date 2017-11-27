using System;

namespace LocalFunctionsBasicSample
{
    /// <summary>
    /// In Section 5.2 we compare the generated IL of these two methods
    /// Both ImplementatioWithDelegates and ImplementationWithLocalFunctions have the same behaviour, 
    /// but the first one is implemented with delegates, the second one is implemented with a local function
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    class ImplementatioWithDelegates
    {
        public void MethodWithDelegates()
        {
            Func<int, int, int> add = (a, b) => a + b;
            int sum = add(3, 5);
            Console.WriteLine(sum);
        }
    }

    class ImplementationWithLocalFunctions
    {
        public void MethodWithLocalMethod()
        {
            int sum = Add(3, 5);
            Console.WriteLine(sum);
            int Add(int a, int b)
            {
                return a + b;
            }
        }
    }
}
