using System.Collections.Generic;

namespace System.Linq
{
   public static class KurtosisOperator
   {
      /// <summary>
      /// Implementation of skewness:
      ///   sigma (y - y_bar) ^ 3 / (n-1) ^ 3
      /// </summary>
      public static double Kurtosis(this IReadOnlyCollection<double> source)
      {
         int n = source.Count;
         double y_bar = source.Sum() / n;
         double sumNumerator = 0;
         double sumDenominator = 0;
         foreach (double y in source)
         {
            sumNumerator += Math.Pow(y - y_bar, 4);
            sumDenominator += Math.Pow(y - y_bar, 2);
         }
         double numerator = sumNumerator / n;
         double denominator = Math.Pow(sumDenominator, 2) / Math.Pow(n, 2);

         return numerator / denominator;
      }
   }
}