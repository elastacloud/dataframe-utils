using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
      private const int MaxDisplay = 10;

      private IList _data;

      public Type DataType { get; }

      public string Name { get; }

      public Series(Type dataType, string name, IList values)
      {
         DataType = dataType;
         Name = name ?? "noname";
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
         var sb = new StringBuilder();
         sb.AppendLine($"{Name} ({DataType}) of {_data.Count}");

         if(MaxDisplay * 2 >= Count)
         {
            sb.AppendLine(string.Join("; ", _data.Cast<object>().Select(i => i.ToString())));
         }
         else
         {
            sb.AppendLine("top " + MaxDisplay + ": ");
            sb.AppendLine(string.Join("; ", _data.Cast<object>().Take(MaxDisplay).Select(i => i.ToString())));

            sb.AppendLine("bottom " + MaxDisplay + ": ");
            sb.AppendLine(string.Join("; ", _data.Cast<object>().Skip(Count - MaxDisplay).Take(MaxDisplay).Select(i => i.ToString())));
         }

         return sb.ToString();
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

      public static implicit operator Series(double[] decimals)
      {
         return new Series(typeof(double), null, decimals);
      }

      public SeriesDescriber Describe()
      {
         return new SeriesDescriber(this);
      }
   }
}
