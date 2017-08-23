using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parquet.Data.Stats
{
    public class SkewnessStatsHandler : StatsHandler
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
          double sum = 0;
          foreach (double y in doubleConvert)
          {
             sum += Math.Pow(y - y_bar, 3);
          }
          values.ColumnSummaryStats.Skewness = sum / ((n - 1) * Math.Pow(values.ColumnSummaryStats.StandardDeviation, 3));
          return values.ColumnSummaryStats;
       }
    }
}
