using System.Linq;
using PDS = Parquet.Data.DataSet;
using DataFrame.Math.Data;
using System.Collections.Generic;

namespace DataFrame.Formats.Parquet
{
   static class ParquetConverter
   {
      public static Frame ConvertFromParquet(PDS pds)
      {
         IEnumerable<Series> allSeries =
            pds.Schema.Elements
               .Select((se, i) => Series.FromList(pds.GetColumn(i), se.ElementType, se.Name));

         return new Frame(allSeries);
      }
   }
}
