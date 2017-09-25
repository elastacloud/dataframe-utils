using System.IO;

namespace DataFrame.Data.Formats
{
   public interface IFormatReader
   {
      Matrix FromStream(Stream inputStream);
   }
}
