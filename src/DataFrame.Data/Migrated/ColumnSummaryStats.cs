/*using System;

namespace Parquet.Data
{
   /// <summary>
   /// General column statistics
   /// </summary>
   public class ColumnSummaryStats
   {
      /// <summary>
      /// Initialises a dataset with a column name
      /// </summary>
      /// <param name="columnName">A dataset columnname</param>
      public ColumnSummaryStats(string columnName)
      {
         ColumnName = columnName;
      }

      /// <summary>
      /// Contains the column name of the stats class
      /// </summary>
      public string ColumnName { get; }

      /// <summary>
      /// Number of null values
      /// </summary>
      public double Sum;

      /// <summary>
      /// Number of null values
      /// </summary>
      public int NullCount;

      /// <summary>
      /// The max value for the column
      /// </summary>
      public double Max;

      /// <summary>
      /// The min value for the column
      /// </summary>
      public double Min;

      /// <summary>
      /// The mean value for the column
      /// </summary>
      public double Mean;

      /// <summary>
      /// The column standard deviation 
      /// </summary>
      public double StandardDeviation;

      /// <summary>
      /// The median value
      /// </summary>
      public double Median;

      /// <summary>
      /// The 25th quartile value
      /// </summary>
      public double Quartile25;

      /// <summary>
      /// The 75th quartile value
      /// </summary>
      public double Quartile75;

      /// <summary>
      /// Illustrates the skew of the normal distribution
      /// </summary>
      public double Skewness;

      /// <summary>
      /// The 4th moment of the series shows how flat the distribution is
      /// </summary>
      public double Kurtosis;

      /// <summary>
      /// Gives the columns variance 
      /// </summary>
      public double Variance;

      /// <summary>
      /// number of distinct values
      /// </summary>
      public double DistinctValuesCount;

      /// <summary>Returns a string that represents the current object.</summary>
      /// <returns>A string that represents the current object.</returns>
      public override string ToString()
      {
         return
            $"ColumnName: {ColumnName}\nNullCount: {NullCount}\nMax: {Max}\nMin: {Min}\nMean: {Mean}\nStandard Deviation: {StandardDeviation}";
      }
   }
}*/