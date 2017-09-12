using System;
using System.Collections.Generic;
using System.Text;

namespace DataFrame.Data.Storage
{
   abstract class MatrixStorage<T>
   {
      protected MatrixStorage(int columns, int rows)
      {

      }

      public abstract T Get(int column, int row);

      public abstract void Set(int column, int row, T value);
   }
}
