using System;
using System.Collections.Generic;
using System.Text;

namespace DataFrame.Math.Data.Storage
{
   /// <summary>
   /// Simple in-memory matrix storage
   /// </summary>
   /// <typeparam name="T"></typeparam>
   sealed class SimpleMatrixStorage<T> : MatrixStorage<T>
   {
      private T[,] _data;

      public SimpleMatrixStorage(int columns, int rows) : base(columns, rows)
      {
         _data = new T[columns, rows];
      }

      public override T Get(int column, int row)
      {
         return _data[column, row];
      }

      public override void Set(int column, int row, T value)
      {
         _data[column, row] = value;
      }

      public override IList<T> GetColumn(int column)
      {
         var columnValues = new List<T>();
         for (int i = 0; i < RowCount; i++)
         {
            columnValues.Add(_data[column, i]);
         }
         return columnValues;
      }

      public override IList<T> GetRow(int row)
      {
         var rowValues = new List<T>();
         for (int i = 0; i < ColumnCount; i++)
         {
            rowValues.Add(_data[i, row]);
         }
         return rowValues;
      }
   }
}
