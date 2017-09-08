using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Linq
{
    public static class SkewnessOperator
    {
       /// <summary>
       /// Implementation of skewness:
       ///   sigma (y - y_bar) ^ 3 / (n-1) ^ 3
       /// </summary>
       public static double Skewness(this IReadOnlyCollection<double> source, double? standardDeviation = null)
       {
          int n = source.Count;

          double y_bar = source.Sum() / n;
          double sum = 0;
          foreach (double y in source)
          {
             sum += Math.Pow(y - y_bar, 3);
          }

          double sd = standardDeviation.HasValue
             ? standardDeviation.Value
             : source.StandardDeviation();

          return 
             sum / ((n - 1) * Math.Pow(sd, 3));
       }
    }
}
