using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DataFrame.Math.Data;
using NetBox.FileFormats;

namespace DataFrame.Formats.Csv
{
   static class CsvFormatReader
   {
      private static readonly Dictionary<Type, Type> _inferredTypeToParquetType = new Dictionary<Type, Type>
      {
         { typeof(byte), typeof(int) }
      };

      /// <summary>
      /// Reads csv stream into dataset
      /// </summary>
      /// <param name="csvStream">CSV stream</param>
      /// <param name="options">Options for reader, optional</param>
      /// <returns>Correct dataset</returns>
      public static Frame ReadToFrame(Stream csvStream, CsvOptions options = null)
      {
         if (csvStream == null) throw new ArgumentNullException(nameof(csvStream));

         if (options == null) options = new CsvOptions();

         var reader = new CsvReader(csvStream, Encoding.UTF8);

         string[] headers = null;
         var columnValues = new Dictionary<int, IList>();

         int rowCount = 0;
         string[] values;
         while ((values = reader.ReadNextRow()) != null)
         {
            //set headers
            if (headers == null)
            {
               if (options.HasHeaders)
               {
                  headers = values;
                  continue;
               }
               else
               {
                  headers = new string[values.Length];
                  for (int i = 0; i < values.Length; i++)
                  {
                     headers[i] = $"Col{i}";
                  }
               }
            }

            //get values
            for (int i = 0; i < values.Length; i++)
            {
               if (!columnValues.TryGetValue(i, out IList col))
               {
                  col = new List<string>();
                  columnValues[i] = col;
               }

               col.Add(values[i]);
            }

            rowCount += 1;
         }

         //set schema
         if (options.InferSchema)
         {
            return InferSchema(headers, rowCount, columnValues);
         }

         return new Frame(headers.Select((name, i) => new Series<string>(name, (List<string>)columnValues[i])));
      }


      private static Frame InferSchema(string[] headers, int rowCount, IReadOnlyDictionary<int, IList> columnValues)
      {
         var series = new List<Series>();
         for (int i = 0; i < headers.Length; i++)
         {
            IList cv = columnValues[i];
            Type columnType = cv.Cast<string>().ToArray().InferType(out IList typedValues);

            Type ct;
            if (!_inferredTypeToParquetType.TryGetValue(columnType, out ct)) ct = columnType;
            series.Add(new Series(ct, headers[i], typedValues));
         }

         return new Frame(series);
      }
   }
}
