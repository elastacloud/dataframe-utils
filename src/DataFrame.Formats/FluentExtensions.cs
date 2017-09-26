using DataFrame.Formats.Parquet;
using DataFrame.Math.Data;

namespace DataFrame.Formats
{
   public static class FluentExtensions
   {
      public static IFormatReader Parquet(this FluentReader entry)
      {
         return new ParquetFormatReader();
      }
   }
}
