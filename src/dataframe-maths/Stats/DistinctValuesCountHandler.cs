using System.Collections;
using System.Linq;

namespace Parquet.Data.Stats
{
   /// <summary>
   /// Used to return the number of null values in the column
   /// </summary>
   public class DistinctValuesCountHandler : StatsHandler
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
         int distinct = values.Values.Cast<object>().Distinct().Count();
         values.ColumnSummaryStats.DistinctValuesCount = distinct;
         return values.ColumnSummaryStats;
      }
   }
}