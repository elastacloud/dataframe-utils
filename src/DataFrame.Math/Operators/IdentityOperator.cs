using System;
using System.Collections.Generic;
using System.Text;
using DataFrame.Math.Data;

namespace DataFrame.Math.Operators
{
    public static class IdentityOperator
    {
      public static Matrix<double> IdentityMatrix(this Matrix<double> mat) {

         var identity = new Matrix<double>(mat.ColumnCount, mat.RowCount);

         for (int r = 0; r < mat.RowCount; r++)
            for (int c = 0; c < mat.ColumnCount; c++)
               identity[r, c] = (r == c) ? 1: 0;

         return identity;
      }
    }
}
