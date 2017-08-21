using System;
using System.Collections;
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
      /// <summary>
      /// Takes in a dataset and ammends the size to the dataset
      /// </summary>
      /// <param name="ds">The Dataset used in parquet-utils based on the parquet schema</param>
       public Matrix(DataSet ds)
       {
          DataSet = new DataSetSummaryStats(ds);
         SetMatrixSize(DataSet.DataSet.RowCount, DataSet.DataSet.ColumnCount);
          for (int i = 0; i < DataSet.DataSet.RowCount; i++)
          {
             for (int j = 0; j < DataSet.DataSet.ColumnCount; j++)
             {
                MatrixType[i, j] = Convert.ToDouble(DataSet.DataSet[i][j]);
             }
          }
       }

       private void SetMatrixSize(int rows, int columns)
       {
          SetMatrixSize(ref MatrixType, rows, columns);
       }

       private void SetMatrixSize(ref double[,] matrix, int rows, int columns)
       {
          matrix = new double[rows, columns];
       }

       /// <summary>
      /// Gets the number of columns in the matrix
      /// </summary>
       public int Cols => MatrixType.GetLength(1);
      /// <summary>
      /// Gets the number of rows in the matrix
      /// </summary>
       public int Rows => MatrixType.GetLength(0);

      public static Matrix GetCovarianceMatrix(DataSet ds)
      {
         var dss = new DataSetSummaryStats(ds);
         var matrix = new Matrix(ds);
         matrix.SetMatrixSize(dss.DataSet.ColumnCount, dss.DataSet.ColumnCount);
         List<double> means = new List<double>(), sums = new List<double>();
         var columnDetails = new List<IList>();
         
         for (int i = 0; i < dss.DataSet.ColumnCount; i++)
         {
            var col = dss.GetColumnStats(i);
            means.Add(col.Mean);
            sums.Add(col.Sum);
            columnDetails.Add(dss.DataSet.GetColumn(i));
         }
         // Cov(x,y) = E{xy} - E{x}E{y}. 
         double covariance = 0.0D;
         for (int rows = 0; rows < dss.DataSet.ColumnCount; rows++)
          {
             for (int cols = 0; cols < dss.DataSet.ColumnCount; cols++)
             {
                // loop in here against the underlying column values
                for (int i = 0; i < dss.DataSet.RowCount; i++)
                {
                   covariance += ((double) columnDetails[rows][i] - means[rows]) *
                                 ((double) columnDetails[cols][i] - means[cols]);
                }
                matrix.MatrixType[rows, cols] = covariance / (double) (dss.DataSet.RowCount - 1);
                covariance = 0.0D;
             }
          }
          return matrix;
       }

       public double this[int i, int j] => MatrixType[i, j];

       public static Matrix GetCorrelationMatrix(DataSet ds)
       {
          var dss = new DataSetSummaryStats(ds);
         var matrix = GetCovarianceMatrix(ds);
          var sd = new List<double>();
          for (int i = 0; i < dss.DataSet.ColumnCount; i++)
          {
             var col = dss.GetColumnStats(i);
             sd.Add(col.StandardDeviation);
          }
          for (int i = 0; i < matrix.MatrixType.GetLength(0); i++)
          {
             for (int j = 0; j < matrix.MatrixType.GetLength(1); j++)
             {
                matrix.MatrixType[i, j] = matrix.MatrixType[i, j] / (sd[i] * sd[j]);
             }
          }
          return matrix;
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
