using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataFrame.Math.Data;

namespace DataFrame.Math.Operators
{
    public static class DotProductOperator
    {
       public static Matrix<double> DotProduct(this Matrix<double> lhs, Matrix<double> rhs)
       {
          if (!CanMultiply(lhs.RowCount, rhs.ColumnCount))
          {
             throw new Exception($"unable to multiply matrices {lhs.RowCount} x {lhs.ColumnCount} and {rhs.RowCount} x {rhs.ColumnCount}");
          }

          var output = new Matrix<double>(lhs.ColumnCount, rhs.RowCount);
         // do the multiplication here
          for (int i = 0; i < lhs.ColumnCount; i++)
          {
             IList<double> columnItems = lhs.GetColumn(i);

             for (int j = 0; j < rhs.RowCount; j++)
             {
                IList<double> rowItems = rhs.GetRow(j);
                double runningTotal = rowItems.Select((t, k) => t * columnItems[k]).Sum();
                output[j, i] = runningTotal;
             }
          }
          return output;
       }

       private static bool CanMultiply(int lhsColumns, int rhsRows)
       {
          return lhsColumns == rhsRows;
       }
    }
}
