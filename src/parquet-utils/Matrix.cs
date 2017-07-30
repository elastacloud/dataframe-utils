using System;
using System.Collections.Generic;
using System.Text;
using Parquet.Data;

namespace parquetutils
{
   /// <summary>
   /// Used to build the Matrix class 
   /// </summary>
    public class Matrix
    {
       internal readonly DataSetSummaryStats DataSet;
       internal double[,] MatrixType;

       public Matrix(DataSetSummaryStats ds)
       {
          DataSet = ds;
       }

       private void SetMatrixSize(int rows, int columns)
       {
          MatrixType = new double[rows - 1,columns - 1];
       }

       public static Matrix GetCovarianceMatrix(DataSetSummaryStats ds)
       {
          var matrix = new Matrix(ds);
          matrix.SetMatrixSize(ds.DataSet.ColumnCount, ds.DataSet.ColumnCount);
          List<double> means = new List<double>(), sums = new List<double>();
          for (int i = 0; i < ds.DataSet.ColumnCount; i++)
          {
             var col = ds.GetColumnStats(i);
             means.Add(col.Mean);
             sums.Add(col.Sum);
          }
          for (int i = 0; i < means.Count; i++)
          {
             for (int j = 0; j < means.Count; j++)
             {
                matrix.MatrixType[i, j] = ((sums[i] * sums[j]) / means.Count) / (means[i] * means[j]);
             }
          }
          return matrix;
       }

       public static Matrix GetCorrelationMatrix(DataSetSummaryStats ds)
       {
          return GetCovarianceMatrix(ds);
       }

      /// <summary>Returns a string that represents the current object.</summary>
      /// <returns>A string that represents the current object.</returns>
      public override string ToString()
       {
          var builder = new StringBuilder();
          builder.AppendLine("\t" + String.Join("\t", DataSet.DataSet.Schema.ColumnNames) + "\n");
          for (int i = 0; i < MatrixType.GetLength(0); i++)
          {
             builder.Append(DataSet.DataSet.Schema.ColumnNames[i] + "\t");
             for (int j = 0; j < MatrixType.GetLength(1); j++)
             {
                builder.Append(MatrixType[i, j] + "\t");
             }
             builder.AppendLine();
          }
          return builder.ToString();
       }
    }
}
