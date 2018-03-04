using System;
using System.Collections.Generic;
using System.Text;
using DataFrame.Math.Data;
using Xunit;

namespace DataFrame.Math.Tests
{
   public class TimeSeriesTest
   {
      [Fact]
      public void TimeSeries_Frequency_True()
      {
         var storage = new TimeSeriesStorage("test",
            new[] {
               new DateTime(2001, 1, 1), new DateTime(2001, 2, 1), new DateTime(2001, 3, 1)
            },
            new[] {
               1.0, 2.0, 3.0
            }
         );

         var ts = new TimeSeries(storage);
         Assert.Equal(ts.Frequency, Frequency.Month);
      }

      [Fact]
      public void TimeSeries_Interpolation_Exception()
      {
         var storage = new TimeSeriesStorage("test",
            new[] {
               new DateTime(2001, 1, 1), new DateTime(2001, 2, 1), new DateTime(2001, 3, 6)
            },
            new[] {
               1.0, 2.0, 3.0
            }
         );

         var ts = new TimeSeries(storage);
         Assert.Throws<TimeSeriesInterpolationException>(() => ts.Frequency);
      }
   }
}