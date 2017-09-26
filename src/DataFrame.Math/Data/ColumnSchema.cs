using System;

namespace DataFrame.Math.Data
{
   public class ColumnSchema<T> : ColumnSchema
   {
      public ColumnSchema(string name) : base(name, typeof(T))
      {
      }
   }

   public class ColumnSchema
   {
      public ColumnSchema(string name, Type dataType)
      {
         Name = name ?? throw new ArgumentNullException(nameof(name));
         DataType = dataType ?? throw new ArgumentNullException(nameof(dataType));
      }

      public string Name { get; }

      public Type DataType { get; }
   }
}
