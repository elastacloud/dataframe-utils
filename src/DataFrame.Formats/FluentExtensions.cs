using System.IO;
using DataFrame.Formats.Csv;
using DataFrame.Formats.Parquet;
using DataFrame.Math.Data;
using Parquet;
using Parquet.Data;

namespace DataFrame.Math.Data
{
   public static class FluentExtensions
   {
      public static Frame Parquet(this FluentReader reader, Stream inputStream, ParquetOptions parquetOptions = null, ReaderOptions readerOptions = null)
      {
         DataSet ds = ParquetReader.Read(inputStream, parquetOptions, readerOptions);

         return ParquetConverter.ConvertFromParquet(ds);
      }

      public static Frame Csv(this FluentReader reader, Stream inputStream, CsvOptions options = null)
      {
         return CsvFormatReader.ReadToFrame(inputStream, options);
      }
   }
}
