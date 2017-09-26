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
         using (FileStream fs = File.OpenRead(@"c:\tmp\athena-spark.parquet"))
         {
            Matrix ds = Matrix.Read().Parquet().FromStream(fs);
         }
      }
   }
}
