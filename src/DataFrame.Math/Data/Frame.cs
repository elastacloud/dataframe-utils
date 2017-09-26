using System.Collections.Generic;
using System.Linq;

namespace DataFrame.Math.Data
{
   /// <summary>
   /// A pandas-like DataFrame
   /// </summary>
   public class Frame
   {
      private readonly List<Series> _series;

      public Frame(IEnumerable<Series> series)
      {
         _series = new List<Series>(series);
      }

      public IReadOnlyCollection<Series> Series => _series;

      public int RowCount => _series.First().Count;

      public Series this[int i]
      {
         get => _series[i];
      }

      public IReadOnlyCollection<object> GetRow(int i)
      {
         return _series.Select(s => s[i]).ToArray();
      }

      public static Frame operator+(Frame f, Series s)
      {
         f._series.Add(s);

         return f;
      }
   }
}
