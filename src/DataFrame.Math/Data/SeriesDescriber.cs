using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataFrame.Math.Data
{
   public class SeriesDescriber
   {
      private readonly Series _series;

      public SeriesDescriber(Series series)
      {
         _series = series ?? throw new ArgumentNullException(nameof(series));
      }

      public Dictionary<string, object> Describe()
      {
         if(_series.DataType.IsNumber())
         {
            List<double> data = _series.Data.Cast<double>().ToList();

            return new Dictionary<string, object>
            {
               ["count"] = data.Count,
               ["mean"] = data.Mean(),
               ["std"] = data.StandardDeviation(),
               ["min"] = data.Min(),
               ["25%"] = data.Quartile25(),
               ["75%"] = data.Quartile75(),
               ["max"] = data.Max()
            };
         }
         else
         {
            return new Dictionary<string, object>
            {
               ["error"] = "not numeric"
            };
         }
      }

      public override string ToString()
      {
         var sb = new StringBuilder();

         foreach(KeyValuePair<string, object> kvp in Describe())
         {
            sb.Append(kvp.Key.PadRight(10));
            sb.AppendLine(kvp.Value.ToString());
         }

         return sb.ToString();
      }
   }
}
