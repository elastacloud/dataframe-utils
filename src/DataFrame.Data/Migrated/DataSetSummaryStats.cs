/*using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Parquet.Data
{
   /// <summary>
   /// Gives a set of summary stats data for the column
   /// </summary>
   public class DataSetSummaryStats
   {
      private readonly DataSet _ds;

      public DataSetSummaryStats(DataSet ds) => _ds = ds;

      public DataSet DataSet => _ds;

      /// <summary>
      /// Gets the column stats needed for the handling of all of the column data
      /// </summary>
      public ColumnSummaryStats GetColumnStats(int index)
      {
         List<double> operableValues = GetColumnData(_ds, index, out int invalidValueCount, out int nullCount);

         return new ColumnSummaryStats(_ds.Schema.ColumnNames[index])
         {
            Sum = operableValues.Sum(),
            NullCount = nullCount,
            Max = operableValues.Max(),
            Min = operableValues.Min(),
            Mean = operableValues.Mean(),
            StandardDeviation = operableValues.StandardDeviation(),
            Median = operableValues.Median(),
            Quartile25 = operableValues.Quartile25(),
            Quartile75 = operableValues.Quartile75(),
            Skewness = operableValues.Skewness(),
            Kurtosis = operableValues.Kurtosis(),
            Variance = operableValues.Variance(),
            DistinctValuesCount = operableValues.Distinct().Count()
         };
      }

      //todo: replaces outs with proper structure
      private List<double> GetColumnData(DataSet ds, int index, out int invalidValueCount, out int nullCount)
      {
         var result = new List<double>();
         int invalids = 0;
         int nulls = 0;

         IList untypedValues = ds.GetColumn(index);

         foreach (object uv in untypedValues)
         {
            if (ReferenceEquals(uv, null))
            {
               nulls += 1;
               continue;
            }

            try
            {
               double v = Convert.ToDouble(uv);
               result.Add(v);
            }
            catch (FormatException)
            {
               invalids += 1;
            }
            catch (InvalidCastException)
            {
               invalids += 1;
            }
         }

         invalidValueCount = invalids;
         nullCount = nulls;
         return result;
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
}*/