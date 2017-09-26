using System.IO;
using DataFrame.Math.Data;
using Parquet;
using PDS = Parquet.Data.DataSet;

namespace DataFrame.Formats.Parquet
{
   class ParquetFormatReader : IFormatReader
   {
      public Frame FromStream(Stream inputStream)
      {
         PDS ds = ParquetReader.Read(inputStream);

         return ParquetConverter.ConvertFromParquet(ds);
      }
   }
}
