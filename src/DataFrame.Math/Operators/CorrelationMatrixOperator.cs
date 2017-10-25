using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using DataFrame.Math.Data;

namespace DataFrame.Math.Operators
{
    public static class CorrelationMatrixOperator
    {
       public static Matrix<double> CorrelationMatrix(this Matrix<double> source)
       {
          var sd = new List<double>();
          for (int i = 0; i < source.ColumnCount; i++)
          {
             sd.Add(new ReadOnlyCollection<double>(source.GetColumn(i)).StandardDeviation());
          }

          var matrix = new Matrix<double>(source.ColumnCount, source.RowCount);
          for (int i = 0; i < source.ColumnCount; i++)
          {
             for (int j = 0; j < source.RowCount; j++)
             {
                matrix[i, j] = source[i, j] / (sd[i] * sd[j]);
             }
          }

          return matrix;
      }


      
   }
}
