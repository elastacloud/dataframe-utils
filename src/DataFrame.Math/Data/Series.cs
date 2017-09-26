using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataFrame.Math.Data
{

   /// <summary>
   /// A panda-like series
   /// </summary>
   public class Series
   {
      private IList _data;

      public Type DataType { get; }

      public string Name { get; }

      public Series(Type dataType, string name)
      {
         DataType = dataType;
         Name = name;
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

         var series = new Series(dataType, name);
         series._data = typedList;

         typedList.AddRange(values);

         return series;
      }
   }
}
