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
   class CsvFormatReader : IFormatReader
   {
      private static readonly Dictionary<Type, Type> _inferredTypeToParquetType = new Dictionary<Type, Type>
      {
         { typeof(byte), typeof(int) }
      };

      public Frame FromStream(Stream inputStream)
      {
         throw new NotImplementedException();
      }

      /// <summary>
      /// Reads csv stream into dataset
      /// </summary>
      /// <param name="csvStream">CSV stream</param>
      /// <param name="options">Options for reader, optional</param>
      /// <returns>Correct dataset</returns>
      public static Matrix<object> ReadToDataSet(Stream csvStream, CsvOptions options = null)
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

         Matrix<object> result;

         //set schema
         if (options.InferSchema)
         {
            result = InferSchema(headers, rowCount, columnValues);
         }
         else
         {
            var schema = headers.Select(h => new ColumnSchema<string>(h)).ToList();
            result = new Matrix(schema, rowCount);
         }

         //assign values
         foreach(KeyValuePair<int, IList> pair in columnValues)
         {
            int r = 0;
            foreach(object value in pair.Value)
            {
               result[pair.Key, r++] = value;
            }
         }

         return result;
      }


      private static Matrix InferSchema(string[] headers, int rowCount, Dictionary<int, IList> columnValues)
      {
         var elements = new List<ColumnSchema>();
         for (int i = 0; i < headers.Length; i++)
         {
            IList cv = columnValues[i];
            Type columnType = cv.Cast<string>().ToArray().InferType(out IList typedValues);

            Type ct;
            if (!_inferredTypeToParquetType.TryGetValue(columnType, out ct)) ct = columnType;
            elements.Add(new ColumnSchema(headers[i], ct));

            columnValues[i] = typedValues;
         }

         return new Matrix(elements, rowCount);
      }
   }
}
