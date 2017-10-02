using System.Collections.Generic;

namespace DataFrame.Math.Data.Storage
{
   public abstract class MatrixStorage<T>
   {
      protected MatrixStorage(int columns, int rows)
      {
         ColumnCount = columns;
         RowCount = rows;
      }

      public abstract T Get(int column, int row);

      public abstract void Set(int column, int row, T value);

      public abstract IList<T> GetColumn(int column);
      public abstract IList<T> GetRow(int row);

      public int ColumnCount { get; private set; }

      public int RowCount { get; private set; }
   }
}