using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;


namespace _02_03_Demo1_PerfTips
{
    public static class Rsi
    {
        public class Change
        {
            public enum Trend { Upward, Downward, Equal }
            public Trend TrendP { get; set; }
            public Decimal ChangeInDec { get; set; }

            public Change(Trend T, Decimal D)
            {
                TrendP = T;
                ChangeInDec = D;
            }
        }

        internal static decimal GetDownwardValue(Rsi.Change changeValue)
        {
            if (changeValue.TrendP == Change.Trend.Downward)
            {
                return changeValue.ChangeInDec;
            }
            return 0m;
        }

        internal static decimal GetUpwardValue(Rsi.Change changeValue)
        {
            if (changeValue.TrendP == Change.Trend.Upward)
            {
                return changeValue.ChangeInDec;
            }
            return 0m;
        }

        public class DateWithChange
        {
            public DateTime Date { get; set; }
            public Change Change { get; set; }

            public DateWithChange(DateTime D, Change C)
            {
                Date = D;
                Change = C;
            }
        }

        static Rsi.DateWithChange GetChange(HistoricalQuote closeDayN, HistoricalQuote closeDayNminus1)
        {

            if (closeDayNminus1.Close < closeDayN.Close)
            {
                return new Rsi.DateWithChange(closeDayN.Date, new Change(Change.Trend.Upward, closeDayN.Close - closeDayNminus1.Close));
            }
            if (closeDayNminus1.Close > closeDayN.Close)
            {
                return new Rsi.DateWithChange(closeDayN.Date, new Rsi.Change(Change.Trend.Downward, closeDayNminus1.Close - closeDayN.Close));
            }

            return new Rsi.DateWithChange(closeDayN.Date, new Change(Change.Trend.Equal, 0));
        }

        public static List<DateWithChange> CalculateChanges(IEnumerable<HistoricalQuote> stockData)
        {
            var retVal = new List<DateWithChange>();

            var stockDataList = stockData.ToList();

            for (int i = 1; i < stockDataList.Count; i++)
            {
                retVal.Add(GetChange(stockDataList[i], stockDataList[i - 1]));
            }
            return retVal;
        }

        static decimal CalculateRsi(decimal avgGain, decimal avgLoss)
        {
            if (avgLoss == decimal.Zero)
            {
                return 100m;
            }
            if (avgGain == decimal.Zero)
            {
                return 0m;
            }
            decimal rs = avgGain / avgLoss;
            return 100m - 100m / (1m + rs);
        }

        public static IEnumerable<Quote> CalculateRsiFixed(IEnumerable<HistoricalQuote> Price, int RsiLength, CancellationToken CT)
        {
            var changes = CalculateChanges(Price);
            var changesInRange = changes.Take(RsiLength);
            int num = changesInRange.Count();

            var changeValuesinRange = changesInRange.Select(n => n.Change);

            CT.ThrowIfCancellationRequested();
            var allGain = 0m;
            foreach (var item in changeValuesinRange)
            {
                allGain += GetUpwardValue(item);
            }

            CT.ThrowIfCancellationRequested();
            var allLost = 0m;
            foreach (var item in changeValuesinRange)
            {
                allLost += GetDownwardValue(item);
            }

            var avgGain = allGain / RsiLength;
            var avgLoss = allLost / RsiLength;

            var newRsiVal = CalculateRsi(avgGain, avgLoss);
            var newRsi = new Quote(changesInRange.Last().Date, newRsiVal);

            CT.ThrowIfCancellationRequested();
            return FixedRsiHelper(changes.Skip(1).ToList(), RsiLength, new List<Quote> { newRsi }, avgGain, avgLoss, CT);
        }

        public static IEnumerable<Quote> CalculateRsi(IEnumerable<HistoricalQuote> Price, int RsiLength, CancellationToken CT)
        {
            var changes = CalculateChanges(Price);
            var changesInRange = changes.Take(RsiLength);
            int num = changesInRange.Count();

            var changeValuesinRange = changesInRange.Select(n => n.Change);

            CT.ThrowIfCancellationRequested();
            var allGain = 0m;
            foreach (var item in changeValuesinRange)
            {
                allGain += GetUpwardValue(item);
            }

            CT.ThrowIfCancellationRequested();
            var allLost = 0m;
            foreach (var item in changeValuesinRange)
            {
                allLost += GetDownwardValue(item);
            }

            var avgGain = allGain / RsiLength;
            var avgLoss = allLost / RsiLength;

            var newRsiVal = CalculateRsi(avgGain, avgLoss);
            var newRsi = new Quote(changesInRange.Last().Date, newRsiVal);

            CT.ThrowIfCancellationRequested();
            return RsiHelper(changes.Skip(1).ToList(), RsiLength, new List<Quote> { newRsi }, avgGain, avgLoss, CT);
        }
        
        private static IEnumerable<Quote> FixedRsiHelper(List<DateWithChange> changes, int rsiLength, List<Quote> res, decimal lastAvgGain, decimal lastAvgLoss, CancellationToken CT)
        {
            int i = 0;
            while (i >= rsiLength)
            {
                decimal currentGain = GetUpwardValue(changes[rsiLength - 1 + i].Change);
                decimal avgGain = (lastAvgGain * (Convert.ToDecimal(rsiLength) - 1m) + currentGain) / Convert.ToDecimal(rsiLength);
                decimal currentLoss = GetDownwardValue(changes[rsiLength - 1 + i].Change);
                decimal avgLoss = (lastAvgLoss * (Convert.ToDecimal(rsiLength) - 1m) + currentLoss) / Convert.ToDecimal(rsiLength);
                decimal newrsiValue = CalculateRsi(avgGain, avgLoss);
                Quote newRsi = new Quote(changes[rsiLength - 1 + i].Date, newrsiValue);
                i++;
                changes = changes.Skip(1).ToList();
                res.Add(newRsi);

                lastAvgLoss = avgLoss;
                lastAvgGain = avgGain;

                CT.ThrowIfCancellationRequested();
            }
            return res;
        }

        private static IEnumerable<Quote> RsiHelper(List<DateWithChange> changes, int rsiLength, List<Quote> res, decimal lastAvgGain, decimal lastAvgLoss, CancellationToken CT)
        {
            while (changes.Count >= rsiLength)
            {
                decimal currentGain = GetUpwardValue(changes[rsiLength - 1].Change);
                decimal avgGain = (lastAvgGain * (Convert.ToDecimal(rsiLength) - 1m) + currentGain) / Convert.ToDecimal(rsiLength);
                decimal currentLoss = GetDownwardValue(changes[rsiLength - 1].Change);
                decimal avgLoss = (lastAvgLoss * (Convert.ToDecimal(rsiLength) - 1m) + currentLoss) / Convert.ToDecimal(rsiLength);
                decimal newrsiValue = CalculateRsi(avgGain, avgLoss);
                Quote newRsi = new Quote(changes[rsiLength - 1].Date, newrsiValue);
                changes = changes.Skip(1).ToList();
                res.Add(newRsi);

                lastAvgLoss = avgLoss;
                lastAvgGain = avgGain;

                CT.ThrowIfCancellationRequested();
            }
            return res;
        }
    }
}
