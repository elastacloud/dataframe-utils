using System;
using System.Collections.Generic;
using System.Text;

namespace DataFrame.Data.Storage
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
   }
}
