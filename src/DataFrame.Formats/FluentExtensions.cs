using DataFrame.Data.Formats;
using DataFrame.Formats.Parquet;

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
