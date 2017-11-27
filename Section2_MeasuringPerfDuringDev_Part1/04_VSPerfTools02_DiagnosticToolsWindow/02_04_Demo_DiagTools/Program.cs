using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace _02_04_Demo1_DiagnosticTools
{
    class Program
    {
        static void Main(string[] args)
        {
            HistoricalPriceReader hpr = new HistoricalPriceReader();

            var historicalData = hpr.GetHistoricalQuotes("MSFT");

            var data = Rsi.CalculateRsi(historicalData, 14, CancellationToken.None);

            for (int i = 0; i < 10; i++)
            {
               Rsi.CalculateRsi(historicalData, 14, CancellationToken.None);

            }
            data = Rsi.CalculateRsi(historicalData, 14, CancellationToken.None);
          
 
            Console.WriteLine("Done"); 
            Console.WriteLine(data.ToList().Count);          
        }
    }
}
