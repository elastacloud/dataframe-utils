using System;
using System.Collections.Generic;
using Parquet.Data;
using Xunit;
using System.Linq;

namespace dftest
{
   public class OperatorsTest
   {
      public class Input
      {
         public Input(double expected, Func<IReadOnlyCollection<double>, double> func, params double[] sequence)
         {
            Expected = expected;
            Func = func;
            Sequence = sequence;
         }

         public double Expected { get; }

         public Func<IReadOnlyCollection<double>, double> Func { get; }

         public double[] Sequence { get; }
      }


      public static IEnumerable<object> OperatorData = new object[]
      {
         new object[] { new Input(320.25, i => i.Variance(), 2, 3, 4, 5, 15, 21, 34, 56 ) },
         new object[] { new Input(3, i => i.Mean(), 2, 3, 4) },
         new object[] { new Input(4.06667, i => i.Mean(), 3.2, 4, 5) },
         new object[] { new Input(1, i => i.StandardDeviation(), 2, 3, 4) },
         new object[] { new Input(0.90185, i => i.StandardDeviation(), 3.2, 4, 5) },
         new object[] { new Input(3.5, i => i.Quartile25(), 1, 3, 4, 6, 7, 10, 19, 87) },
         new object[] { new Input(6.5, i => i.Median(), 1, 3, 4, 6, 7, 10, 19, 87) },
         new object[] { new Input(14.5, i => i.Quartile75(), 1, 3, 4, 6, 7, 10, 19, 87) },
         new object[] { new Input(-0.09621, i => i.Skewness(), 5, 20, 40, 80, 100, 102) },
         new object[] { new Input(1.34065, i => i.Kurtosis(), 5, 20, 40, 80, 100, 102) },
      };

      [Theory]
      [MemberData(nameof(OperatorData))]
      public void Operator_calculations_are_correct(Input input)
      {
         double actual = input.Func(input.Sequence);

         Assert.Equal(input.Expected, actual, 5);
      }
   }
}
