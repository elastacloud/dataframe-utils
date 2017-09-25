namespace DataFrame.Data.Storage
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

      public int ColumnCount { get; private set; }

      public int RowCount { get; private set; }
   }
}