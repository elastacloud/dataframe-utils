using System;
using System.Collections.Generic;
using System.Text;
using DataFrame.Math.Data;

namespace DataFrame.Math.Operators
{
   public static class InverseMatrixOperator
   {
      public static Matrix<double> InverseMatrix(this Matrix<double> mat)
      {
         if (mat.ColumnCount != mat.RowCount)
            throw new Exception($"unable to inverse non-square matrix {mat.RowCount} x {mat.ColumnCount}");

         double determinant = GetMatrixDeterminant(mat);

         if (determinant == 0)
            throw new Exception($"matrix in singular, determinant = {determinant}");

         var inv = new Matrix<double>(mat.ColumnCount,mat.RowCount);

         //special case for 2x2 matrix:
         if (mat.ColumnCount == 2)
            {
               inv.AddRow(0, new Series<double>("a", new double[] 
                  { mat[1,1] / determinant, -1 * mat[0,1] / determinant }));
               inv.AddRow(1, new Series<double>("b", new double[]
                  { -1*mat[1,0]/determinant, mat[0,0]/determinant}));
            return inv;
            }

         // find matrix of cofactors
         var cofactors = new Matrix<double>(mat.ColumnCount,mat.RowCount);

         for (int r = 0; r < mat.RowCount; r++) {
            double[] cofactorRow = new double[mat.RowCount];
            for (int c = 0; c < mat.ColumnCount; c++) {
               Matrix<double> minor = GetMatrixMinor(mat, r, c);
               cofactorRow.SetValue(System.Math.Pow(-1,r + c) * GetMatrixDeterminant(minor),c);
             
            }
            cofactors.AddRow(r, new Series<double>($"Row {r}",cofactorRow));
         }

         cofactors = cofactors.Transpose();

         for (int r = 0; r < mat.ColumnCount; r++)
            for (int c = 0; c < mat.RowCount; c++)
               cofactors[r, c] = cofactors[r, c] / determinant;
         
         return cofactors;
      }

      public static Matrix<double> GetMatrixMinor(this Matrix<double> mat, int row, int col)
      {

         var rowCut = new Matrix<double>(mat.ColumnCount, mat.RowCount - 1);
         var colCut = new Matrix<double>(mat.RowCount - 1, mat.ColumnCount);
         var minorT = new Matrix<double>(mat.RowCount - 1, mat.ColumnCount - 1);

         int r = 0;

         // row Cut
         for (int i = 0; i < mat.RowCount; i++)
         {
            if (i != row)
            {
               rowCut.AddRow(r, mat.GetRow(i));
               r += 1;
            }
         }

         //reset r
         r = 0;
         // transpose and cut again for column
         colCut = rowCut.Transpose();

         for (int i = 0; i < mat.ColumnCount; i++)
         {
            if (i != col)
            {
               minorT.AddRow(r, colCut.GetRow(i));
               r += 1;
            }
         }

         return minorT.Transpose();
      }

      public static double GetMatrixDeterminant(this Matrix<double> m)
      {
         // base case for 2x2 matrix
         if (m.ColumnCount == 2)
            return m[0, 0] * m[1, 1] - m[0, 1] * m[1, 0];
         
         double determinant = 0;

         for (int c = 0; c < m.ColumnCount; c++)
            determinant += System.Math.Pow(-1,c) * m[0,c] * GetMatrixDeterminant(GetMatrixMinor(m, 0, c));

         return determinant;
      }
   }
}
