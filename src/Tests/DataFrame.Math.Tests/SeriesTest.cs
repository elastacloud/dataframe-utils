using System;
using System.Collections.Generic;
using System.Text;
using DataFrame.Math.Data;
using Xunit;

namespace DataFrame.Math.Tests
{
   public class SeriesTest
   {
      [Fact]
      public void DescribeTest()
      {
         Series s = new double[] { 1, 2, 3, 4, 5 };

         string str = s.Describe().ToString();
      }
   }
}
