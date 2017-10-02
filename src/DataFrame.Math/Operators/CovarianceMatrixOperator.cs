using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using DataFrame.Math.Data;

namespace DataFrame.Math.Operators
{
    public static class CovarianceMatrixOperator
    {
       public static Matrix<double> CovarianceMatrix(this Matrix<double> source)
       {
          List<double> means = new List<double>(), sums = new List<double>();

          var columnDetails = new List<IList>();

          for (int i = 0; i < source.ColumnCount; i++)
          {
             var col = new ReadOnlyCollection<double>(source.GetColumn(i));
             means.Add(col.Mean());
             sums.Add(col.Sum());
             columnDetails.Add(col);
          }
          // Cov(x,y) = E{xy} - E{x}E{y}. 
          double covariance = 0.0D;
          var matrix = new Matrix<double>(source.ColumnCount, source.RowCount);
          for (int rows = 0; rows < source.ColumnCount; rows++)
          {
             for (int cols = 0; cols < source.ColumnCount; cols++)
             {
                // loop in here against the underlying column values
                for (int i = 0; i < source.RowCount; i++)
                {
                   covariance += ((double) columnDetails[rows][i] - means[rows]) *
                                 ((double) columnDetails[cols][i] - means[cols]);
                }

                matrix[rows, cols] = covariance / (double) (source.RowCount - 1);
                covariance = 0.0D;
             }
          }

          return matrix;
       }
    }
}
