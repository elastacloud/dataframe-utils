using System;
using System.Collections.Generic;
using System.Text;

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
   }
}
