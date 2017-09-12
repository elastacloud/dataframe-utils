using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace System.Linq
{
   /// <summary>
   /// Used to return the min value of the column
   /// </summary>
   public static class VarianceOperator
   {
      /// <summary>
      /// Gets the count of null values given the list of column values
      /// </summary>
      /// <param name="values">A list of values</param>
      /// <returns>A count of null values</returns>
      public static double Variance(this IReadOnlyCollection<double> source)
      {
         double count = source.Count;
         double sum = source.Sum();
         double average = sum / count;
         double varianceSum = source.Sum(item => Math.Pow(Convert.ToDouble(item) - average, 2));
         return varianceSum / count;
      }
   }
}