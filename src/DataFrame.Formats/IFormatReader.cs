using System.IO;
using DataFrame.Math.Data;

namespace DataFrame.Formats
{
   public interface IFormatReader
   {
      Matrix FromStream(Stream inputStream);
   }
}
