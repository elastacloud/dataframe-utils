using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataFrame.Data;
using PDS = Parquet.Data.DataSet;
using PDR = Parquet.Data.Row;

namespace DataFrame.Formats.Parquet
{
   static class ParquetConverter
   {
      public static Matrix ConvertFromParquet(PDS pds)
      {
         var schema = pds.Schema.Elements.Select(se => new ColumnSchema(se.Name, se.ElementType)).ToList();
         var result = new Matrix(schema, pds.RowCount);

         int r = 0;
         foreach(PDR row in pds)
         {
            object[] raw = row.RawValues;
            for(int c = 0; c < raw.Length; c++)
            {
               result[c, r] = raw[c];
            }
            r += 1;
         }

         return result;
      }
   }
}
