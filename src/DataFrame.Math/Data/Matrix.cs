using System.Collections.Generic;
using System.Text;
using DataFrame.Math.Data.Storage;

namespace DataFrame.Math.Data
{
   public class Matrix : Matrix<object>
   {
      public Matrix(IReadOnlyCollection<ColumnSchema> schema, int rowCount) : base(schema.Count, rowCount)
      {
      }
   }

   public class Matrix<T>
   {
      private readonly MatrixStorage<T> _storage;
      private readonly List<ColumnSchema> _schema = new List<ColumnSchema>();

      public Matrix(int columns, int rows) : this(columns, rows, new SimpleMatrixStorage<T>(columns, rows))
      {

      }

      public Matrix(int columns, int rows, MatrixStorage<T> storage)
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

      public int ColumnCount => _storage.ColumnCount;

      public int RowCount => _storage.RowCount;

      private void ValidateIndexInRange(int column, int row)
      {
         //todo:
      }

      #region [ Fluent Extensions ]

      public static FluentReader Read()
      {
         return new FluentReader();
      }

      #endregion

      public override string ToString()
      {
         var sb = new StringBuilder();

         //get max lengths for column and create string matrix
         var sm = new Matrix<string>(ColumnCount, RowCount);
         int[] maxLen = new int[ColumnCount];

         for(int c = 0; c < ColumnCount; c++)
         {
            int ml = 0;

            for(int r = 0; r < RowCount; r++)
            {
               object v = this[c, r];
               if(v != null)
               {
                  string sv = v.ToString();
                  if (sv.Length > ml) ml = sv.Length;
                  sm[c, r] = sv;
               }
            }
         }

         //print matrix

         for(int r = 0; r < RowCount; r++)
         {
            for(int c = 0; c < ColumnCount; c++)
            {
               string sv = (sm[c, r] ?? string.Empty).PadLeft(maxLen[c]);
               sb.Append(sv);
               if (c + 1 < ColumnCount) sb.Append(" | ");
            }

            sb.AppendLine();
         }

         return sb.ToString();
      }
   }
}
