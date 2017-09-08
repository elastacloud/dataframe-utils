using System.Collections.Generic;

namespace System.Linq
{
   /// <summary>
   /// Used to create a quartile handler giving the values either side of the median and 25/75
   /// </summary>
   public static class QuartileOperator
   {
      public static double Median(this IReadOnlyCollection<double> source)
      {
         var ordered = new List<double>(source);
         ordered.Sort();

         int n = source.Count;
         int midpoint = n / 2;

         return midpoint % 2 == 0
            ? (ordered[midpoint - 1] + ordered[midpoint]) / 2D
            : ordered[midpoint];
      }

      public static double Quartile25(this IReadOnlyCollection<double> source)
      {
         var ordered = new List<double>(source);
         ordered.Sort();

         int n = source.Count;
         int midpoint = n / 2;

         double quartile25 = 0D;

         // This is an even case for the quartile
         if (midpoint % 2 == 0)
         {
            quartile25 = (ordered[midpoint / 2 - 1] + ordered[midpoint / 2]) / 2D;
         }
         else if ((n - 1) % 4 == 0)
         {
            int nMod = (n - 1) / 4;
            quartile25 = (ordered[nMod - 1] * 0.25) + (ordered[nMod] * 0.75);
         }
         else if ((n - 3) % 4 == 0)
         {
            int nMod = (n - 3) / 4;
            quartile25 = (ordered[nMod] * .75) + (ordered[nMod + 1] * .25);
         }
         return quartile25;
      }

      public static double Quartile75(this IReadOnlyCollection<double> source)
      {
         var ordered = new List<double>(source);
         ordered.Sort();

         int n = source.Count;
         int midpoint = n / 2;

         double quartile75 = 0D;

         // This is an even case for the quartile
         if (midpoint % 2 == 0)
         {
            quartile75 = (ordered[midpoint + (midpoint / 2) - 1] +
                          ordered[midpoint + (midpoint / 2)]) / 2D;
         }
         else if ((n - 1) % 4 == 0)
         {
            int nMod = (n - 1) / 4;
            quartile75 = (ordered[3 * nMod] * 0.75) + (ordered[3 * nMod + 1] * 0.25);
         }
         else if ((n - 3) % 4 == 0)
         {
            int nMod = (n - 3) / 4;
            quartile75 = (ordered[3 * nMod + 1] * .25) + (ordered[3 * nMod + 2] * .75);
         }
         return quartile75;
      }
   }
}
