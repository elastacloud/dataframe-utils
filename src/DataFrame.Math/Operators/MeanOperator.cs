using System.Collections.Generic;

namespace System.Linq
{
   /// <summary>
   /// Used to return the min value of the column
   /// </summary>
   public static class MeanOperator
   {
      /// <summary>
      /// Gets the count of null values given the list of column values
      /// </summary>
      /// <param name="source">A list of values</param>
      /// <returns>A count of null values</returns>
      public static double Mean(this IReadOnlyCollection<double> source)
      {
         double count = source.Count;
         double sum = source.Sum();
         double mean = sum / count;
         return mean;
      }
   }
}