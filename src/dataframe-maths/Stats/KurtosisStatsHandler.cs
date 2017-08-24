using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parquet.Data.Stats
{
    public class KurtosisStatsHandler : StatsHandler
    {
       /// <summary>
       /// Implementation of skewness:
       ///   sigma (y - y_bar) ^ 3 / (n-1) ^ 3
       /// </summary>
       public override ColumnSummaryStats GetColumnStats(ColumnStatsDetails values)
       {
          var doubleConvert = values.Values.Cast<object>().Select(Convert.ToDouble).ToList();
          int n = doubleConvert.Count;
          // ReSharper disable once InconsistentNaming
          double y_bar = doubleConvert.Sum() / n;
          double sumNumerator = 0;
          double sumDenominator = 0;
          foreach (double y in doubleConvert)
          {
             sumNumerator += Math.Pow(y - y_bar, 4);
             sumDenominator += Math.Pow(y - y_bar, 2);
          }
          double numerator = sumNumerator / n;
          double denominator = Math.Pow(sumDenominator, 2) / Math.Pow(n, 2);

          values.ColumnSummaryStats.Kutosis = numerator / denominator;
          return values.ColumnSummaryStats;
       }
    }
}
