using System;
using System.Collections.Generic;
using Parquet.Data;
using Parquet.Data.Stats;

namespace Parquet.Data
{
   /// <summary>
   /// Gives a set of summary stats data for the column
   /// </summary>
   public class DataSetSummaryStats
   {
      private readonly Parquet.Data.DataSet _ds;
      private readonly Dictionary<StatsHandler, Type[]> _handlers = new Dictionary<StatsHandler, Type[]>();
      /// <summary>
      /// Sets up the available stats handlers -this needs to turn into a discovery
      /// </summary>
      public DataSetSummaryStats(DataSet ds)
      {
         _ds = ds;
         _handlers.Add(new MaxStatsHandler(), NumericTypes);
         _handlers.Add(new NullStatsHandler(), AllTypes);
         _handlers.Add(new MinStatsHandler(), NumericTypes);
         _handlers.Add(new MeanStatsHandler(), NumericTypes);
         _handlers.Add(new StdDevHandler(), NumericTypes);
         _handlers.Add(new VarianceStatsHandler(), NumericTypes);
         _handlers.Add(new SumHandler(), NumericTypes);
         _handlers.Add(new QuartileStatsHandler(), NumericTypes);
         _handlers.Add(new SkewnessStatsHandler(), NumericTypes);
         _handlers.Add(new KurtosisStatsHandler(), NumericTypes);
      }
      /// <summary>
      /// Gets the dataset currently stored 
      /// </summary>
      public DataSet DataSet => _ds;

      private Type[] NumericTypes => new Type[] {typeof(double), typeof(int), typeof(float), typeof(long)};
      private Type[] AllTypes => new Type[] { typeof(double), typeof(int), typeof(float), typeof(long), typeof(string), typeof(DateTimeOffset) };
      /// <summary>
      /// Gets the column stats needed for the handling of all of the column data
      /// </summary>
      public ColumnSummaryStats GetColumnStats(int index)
      {
         var stats = new ColumnSummaryStats(_ds.Schema.ColumnNames[index]);
         var columns = _ds.GetColumn(index);
         
         foreach (var handler in _handlers)
         {
            // This should be responsible for it's own types
            handler.Key.GetColumnStats(new ColumnStatsDetails(columns, stats, handler.Value, _ds.Schema.Elements[index].ElementType));
         }

         return stats;
      }
      /// <summary>
      /// Gets the column stats needed for the handling of all of the column data
      /// </summary>
      public ColumnSummaryStats GetColumnStats(SchemaElement schema)
      {
         int index = _ds.Schema.GetElementIndex(schema);
         return GetColumnStats(index);
      }
   }
}
