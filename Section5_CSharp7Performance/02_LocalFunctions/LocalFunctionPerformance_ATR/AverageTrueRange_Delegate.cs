using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LocalFunctionsPerformanceSample;

namespace LocalFunctionPerformance_SampleApp
{
    /// <summary>
    /// Calculates ATR by using Func<T> in the TrueRange function
    /// </summary>
    class AverageTrueRange_Delegate
    {
        public static decimal TrueRange(HistoricalValue current, HistoricalValue prev)
        {
            Func<decimal> AbsMax = () =>
            {
                return Math.Max(Math.Abs(current.High - prev.Close), Math.Abs(current.Low - prev.Close));
            };

            return Math.Max((current.High - prev.Close), AbsMax());                      
        }

        public static List<Quote> CalculateAverageTrueRange_Delegate(HistoricalValue[] Price, int Length)
        {
            var retVal = new List<Quote>();
            var trueRanges = new List<Quote>();

            var firstTr = Price[0].High - Price[0].Low;
            trueRanges.Add(new Quote(Price[0].Date, firstTr));

            for (int i = 1; i < Price.Length; i++)
            {
                var newTrueRange = TrueRange(Price[i - 1], Price[i]);
                trueRanges.Add(new Quote(Price[i].Date, newTrueRange));
            }

            var firstatr = trueRanges.Take(Length - 1).Average(n => n.Value);
            retVal.Add(new Quote { Value = firstatr, Date = Price[Length - 1].Date });

            Decimal prevAtr = firstatr;

            for (int i = Length; i < trueRanges.Count; i++)
            {
                var atr = (prevAtr * (Length - 1) + trueRanges[i].Value) / Length;
                retVal.Add(new Quote { Value = atr, Date = trueRanges[i].Date });
                prevAtr = atr;
            }

            return retVal;
        }
    }
}
