using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataFrame.Math.Data;

namespace DataFrame.Math.Operators
{
   public static class TransposeOperator
   {
      public static Matrix<double> Transpose(this Matrix<double> mat)
      {

         var output = new Matrix<double>(mat.RowCount, mat.ColumnCount);
         // do transpose here
         for (int i = 0; i < mat.ColumnCount; i++)
            for (int j = 0; j < mat.RowCount; j++)
               output[j, i] = mat[i,j];

         return output;
      }
    }
}
