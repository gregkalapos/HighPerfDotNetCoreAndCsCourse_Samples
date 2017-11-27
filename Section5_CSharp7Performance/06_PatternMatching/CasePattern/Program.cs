using System;

namespace CasePattern
{
    class Program
    {
        static void Main(string[] args)
        {           
            CaseWithMultipleTypes(60);
        }

        public static void CaseWithMultipleTypes(object param)
        {
            switch (param)
            {
                case int i when i > 50:
                    Console.WriteLine("param: int and > 50");
                    break;
                case int i when i > 100:
                    Console.WriteLine("param: int and > 100");
                    break;
                case int i:
                    Console.WriteLine("param: int and < 50");
                    break;
                case string str:
                    Console.WriteLine($"param is string: {str}");
                    break;
                case var v:
                    Console.WriteLine($"Unknown type: {v.GetType()}");
                    break;
            }
        }
    }
}
