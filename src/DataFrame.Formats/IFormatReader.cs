using System.IO;
using DataFrame.Math.Data;

namespace DataFrame.Formats
{
   public interface IFormatReader
   {
      Frame FromStream(Stream inputStream);
   }
}
