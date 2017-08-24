using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Parquet.Data.Stats
{
   /// <summary>
   /// Used to create a quartile handler giving the values either side of the median and 25/75
   /// </summary>
   public class QuartileStatsHandler : StatsHandler
   {
      /// <summary>
      /// Gets the columns stats 
      /// </summary>
      /// <param name="values">An unordered values collection from the dataframe column</param>
      /// <returns>A column summary stats instance</returns>
      public override ColumnSummaryStats GetColumnStats(ColumnStatsDetails values)
      {
         double quartile25 = 0D, median = 0D, quartile75 = 0D;

         var ordered = values.Values.Cast<object>().Select(Convert.ToDouble).OrderBy(x => x).ToList();
         int n = values.Values.Count;
         int midpoint = n / 2;
         // This is an even case for the quartile
         if (midpoint % 2 == 0)
         {
            median = (ordered[midpoint - 1] + ordered[midpoint]) / 2D;
            //easy split 
            if (midpoint % 2 == 0)
            {
               quartile25 = (ordered[midpoint / 2 - 1] + ordered[midpoint / 2]) / 2D;
               quartile75 = (ordered[midpoint + (midpoint / 2) - 1] +
                             ordered[midpoint + (midpoint / 2)]) / 2D;
            }
            else
            {
               quartile25 = ordered[midpoint / 2];
               quartile75 = ordered[(midpoint / 2) + midpoint];
            }
         }
         else
         {
            median = ordered[midpoint];

            if ((n - 1) % 4 == 0)
            {
               int nMod = (n - 1) / 4;
               quartile25 = (ordered[nMod - 1] * 0.25) + (ordered[nMod] * 0.75);
               quartile75 = (ordered[3 * nMod] * 0.75) + (ordered[3 * nMod + 1] * 0.25);
            }
            else if ((n - 3) % 4 == 0)
            {
               int nMod = (n - 3) / 4;
               quartile25 = (ordered[nMod] * .75) + (ordered[nMod + 1] * .25);
               quartile75 = (ordered[3 * nMod + 1] * .25) + (ordered[3 * nMod + 2] * .75);
            }
         }
         values.ColumnSummaryStats.Quartile25 = quartile25;
         values.ColumnSummaryStats.Quartile75 = quartile75;
         values.ColumnSummaryStats.Median = median;
         return values.ColumnSummaryStats;
      }
   }

   /// <summary>
   /// Represents the three different types of quartile possible
   /// </summary>
   public enum QuartileType
   {
      /// <summary>
      /// Less than 25% of the population 
      /// </summary>
      Quartile25 = 1,
      /// <summary>
      /// 50% of the population
      /// </summary>
      Median = 2,
      /// <summary>
      /// 75% of the population
      /// </summary>
      Quartile75 = 3
   }
}
