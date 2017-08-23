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
          var doubleConvert = values.Values.Cast<double>().ToList();
          int n = doubleConvert.Count;
          // ReSharper disable once InconsistentNaming
          double y_bar = doubleConvert.Sum() / n;
          double sum_numerator = 0;
          double sum_denominator = 0;
          foreach (double y in doubleConvert)
          {
             sum_numerator += Math.Pow(y - y_bar, 4);
             sum_denominator += Math.Pow(y - y_bar, 2);
          }
          double numerator = sum_numerator / n;
          double denominator = Math.Pow(sum_denominator, 2) / Math.Pow(n, 2);

          values.ColumnSummaryStats.Kutosis = numerator / denominator;
          return values.ColumnSummaryStats;
       }
    }
}
