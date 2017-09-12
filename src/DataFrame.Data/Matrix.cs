using System;
using System.Collections.Generic;
using System.Text;
using DataFrame.Data.Storage;

namespace DataFrame.Data
{
   public class Matrix<T>
   {
      private readonly MatrixStorage<T> _storage;

      public Matrix(int columns, int rows) : this(columns, rows, new SimpleMatrixStorage<T>(columns, rows))
      {

      }

      private Matrix(int columns, int rows, MatrixStorage<T> storage)
      {
         _storage = storage;
      }

      public T this[int column, int row]
      {
         get
         {
            ValidateIndexInRange(column, row);

            return _storage.Get(column, row);
         }
         set
         {
            ValidateIndexInRange(column, row);

            _storage.Set(column, row, value);
         }
      }

      private void ValidateIndexInRange(int column, int row)
      {

      }
   }
}
