using System.IO;
using DataFrame.Math.Data;
using Xunit;

namespace DataFrame.Formats.Tests
{
   public class FormatReadingTest
   {
      [Fact]
      public void Read_parquet_file()
      {
         using (FileStream fs = File.OpenRead(GetDataFilePath("alltypes.snappy.parquet")))
         {
            Frame frame = Matrix.Read().Parquet().FromStream(fs);
         }
      }

      private string GetDataFilePath(string name)
      {
         string thisPath = GetType().Assembly.Location;
         return Path.Combine(Path.GetDirectoryName(thisPath), "TestData", name);
      }
   }
}
