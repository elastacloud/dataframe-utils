using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataFrame.Math.Data
{
   /// <summary>
   /// Used to build a time series with available storage for the 
   /// </summary>
    public class TimeSeries 
    {
      /// <summary>
      /// Returns a time series storage 
      /// </summary>
       public TimeSeriesStorage Storage { get; private set; }
      /// <summary>
      /// Constructs a time series
      /// </summary>
      /// <param name="storage">A time series storage containing intervals and values</param>
       public TimeSeries(TimeSeriesStorage storage)
       {
          Storage = storage;
       }
      /// <summary>
      /// Supplies frequency if all values are uniform
      /// </summary>
       public Frequency Frequency
       {
          get
          {
             if (!CheckFrequency(out var frequency))
             {
                throw new TimeSeriesInterpolationException(frequency);
             }
             return frequency;
          }
       }

       private bool CheckFrequency(out Frequency frequency)
       {
          DateTime? previousDt = null;
          frequency = Frequency.Unknown;
          foreach (DateTime dt in Storage.Intervals.Data)
          {

             frequency = Frequency.Unknown;

             if (previousDt == null)
             {
                previousDt = dt;
                continue;
             }
             if ((dt - previousDt.Value) == TimeSpan.FromHours(1))
             {
                frequency = Frequency.Hour;
             }
             else if ((dt - previousDt.Value) == TimeSpan.FromDays(1))
             {
                frequency = Frequency.Day;
             }
             else if ((dt - previousDt.Value) == TimeSpan.FromDays(7))
             {
                frequency = Frequency.Week;
             }
             else if ((dt - previousDt.Value) == TimeSpan.FromDays(365))
             {
                frequency = Frequency.Year;
             }
             else if ((dt - previousDt.Value) == TimeSpan.FromDays(366))
             {
                frequency = Frequency.Year;
             }
             else if ((dt - previousDt.Value) == TimeSpan.FromDays(28))
             {
                frequency = Frequency.Month;
             }
             else if ((dt - previousDt.Value) == TimeSpan.FromDays(29))
             {
                frequency = Frequency.Month;
             }
             else if ((dt - previousDt.Value) == TimeSpan.FromDays(30))
             {
                frequency = Frequency.Month;
             }
             else if ((dt - previousDt.Value) == TimeSpan.FromDays(31))
             {
                frequency = Frequency.Month;
             }
             else
             {
                return false;
             }
             previousDt = dt;
          }
          return true;
       }
    }
   /// <summary>
   /// Storage class for time series values at frequent intervals
   /// </summary>
   public class TimeSeriesStorage
   {
      /// <summary>
      /// Used to hold the data for a time series
      /// </summary>
      /// <param name="name"></param>
      /// <param name="observationDates"></param>
      /// <param name="values"></param>
      public TimeSeriesStorage(string name, IEnumerable<DateTime> observationDates, IEnumerable<double> values)
      {
         Name = name;
         Values = new Series<double>($"{name}--values", values.ToList());
         Intervals = new Series<DateTime>($"{name}--intervals", observationDates.ToList());
      }
      /// <summary>
      /// The datetime interval values that are used
      /// </summary>
      public Series<DateTime> Intervals { get; private set; }
      /// <summary>
      /// The underlying numbers that for the time series
      /// </summary>
      public Series<double> Values { get; private set; }
      /// <summary>
      /// The name of the time series
      /// </summary>
      public string Name { get; private set; }
   }
   /// <summary>
   /// The time series frequency used for the frequency argument
   /// </summary>
   public enum Frequency
   {
      Hour,
      Day, 
      Week,
      Month,
      Year,
      Unknown
   }

   /// <summary>
   /// Used when frequency isn't exact leading to interpolation errors
   /// </summary>
   public class TimeSeriesInterpolationException : Exception
   {
      private readonly Frequency _frequency;
      public TimeSeriesInterpolationException(Frequency frequency)
      {
         _frequency = frequency;
      }

      public override string Message => $"Interpolation not available for this time series - assumed frequency is {_frequency} but is not exact. Please correct and try again.";
   }
}
