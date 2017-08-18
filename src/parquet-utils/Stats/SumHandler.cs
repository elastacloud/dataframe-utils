using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parquet.Data;
using Parquet.Data.Stats;

namespace Parquet.Data.Stats
{
/// <summary>
/// Used to return the min value of the column
/// </summary>
public class SumHandler : StatsHandler
{
   /// <summary>
   /// Gets the count of null values given the list of column values
   /// </summary>
   /// <param name="values">A list of values</param>
   /// <returns>A count of null values</returns>
   public override ColumnSummaryStats GetColumnStats(ColumnStatsDetails values)
   {
      if (!CanCalculateWithType(values))
         return values.ColumnSummaryStats;
      double sum = values.Values.Cast<object>().Sum(t => Convert.ToDouble(t));

      values.ColumnSummaryStats.Sum = sum;
      return values.ColumnSummaryStats;
   }

}
}
