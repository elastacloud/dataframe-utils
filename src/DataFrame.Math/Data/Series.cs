using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataFrame.Math.Data
{

   public class Series<T> : Series
   {
      public Series(string name, IEnumerable<T> values) : base(typeof(T), name, new List<T>(values))
      {
      }

      public new IList<T> Data => (List<T>)base.Data;
   }

   /// <summary>
   /// A panda-like series
   /// </summary>
   public class Series
   {
      private IList _data;

      public Type DataType { get; }

      public string Name { get; }

      public Series(Type dataType, string name, IList values)
      {
         DataType = dataType;
         Name = name;
         _data = values;
      }

      public IList Data => _data;

      public int Count => _data.Count;

      public object this[int i]
      {
         get => _data[i];
      }

      public override string ToString()
      {
         return $"{Name} ({DataType}) of {_data.Count}";
      }

      public static Series FromList(IList values, Type dataType, string name)
      {
         Type listType = typeof(List<>);
         Type listGenericType = listType.MakeGenericType(dataType);

         IList typedList = (IList)Activator.CreateInstance(listGenericType);

         var series = new Series(dataType, name, typedList);

         typedList.AddRange(values);

         return series;
      }
   }
}
