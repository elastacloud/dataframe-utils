using System;
using parquetutils;
using Parquet.Data;
using Xunit;

namespace dftest
{
   public class DataSetSummaryTest
   {
      [Fact]
      public void Check_datasetstats_numerical_max()
      {
         var ds = new DataSet(new SchemaElement<string>("s"), new SchemaElement<int>("i"),
            new SchemaElement<float>("f")) { { "1", 2, 3F }, { "1", 3, 4F }, { "1", 4, 5.5F } };

         var summary = new DataSetSummaryStats(ds);
         Assert.Equal(4, summary.GetColumnStats(1).Max);
         Assert.Equal(5.5, summary.GetColumnStats(2).Max);
      }

      [Fact]
      public void Check_datasetstats_duplicates()
      {
         var ds = new DataSet(new SchemaElement<string>("s"), new SchemaElement<int>("i"),
            new SchemaElement<float>("f")) { { "1", 2, 3F }, { "1", 3, 3F }, { "1", 4, 5.5F } };

         var summary = new DataSetSummaryStats(ds);
         Assert.Equal(1, summary.GetColumnStats(0).DistinctValuesCount);
         Assert.Equal(3, summary.GetColumnStats(1).DistinctValuesCount);
         Assert.Equal(2, summary.GetColumnStats(2).DistinctValuesCount);
      }

      [Fact]
      public void Check_datasetstats_variance()
      {
         var ds = new DataSet(new SchemaElement<int>("y"))
         { { 2 }, { 3 }, { 4 }, { 5 }, { 15 }, { 21 }, { 34 }, { 56 } };

         var summary = new DataSetSummaryStats(ds);
         Assert.Equal(320.25, summary.GetColumnStats(0).Variance);
      }

      [Fact]
      public void Check_datasetstats_numerical_min()
      {
         var ds = new DataSet(new SchemaElement<string>("s"), new SchemaElement<int>("i"),
            new SchemaElement<float>("f")) { { "1", 2, 3F }, { "1", 3, 4F }, { "1", 4, 5F } };

         var summary = new DataSetSummaryStats(ds);

         Assert.Equal(2, summary.GetColumnStats(1).Min);
         Assert.Equal(3, summary.GetColumnStats(2).Min);
      }

      [Fact]
      public void Check_datasetstats_numerical_mean()
      {
         var ds = new DataSet(new SchemaElement<string>("s"), new SchemaElement<int>("i"),
            new SchemaElement<float>("f")) { { "1", 2, 3.2F }, { "1", 3, 4F }, { "1", 4, 5F } };

         var summary = new DataSetSummaryStats(ds);
         Assert.Equal(3, summary.GetColumnStats(1).Mean);
         Assert.Equal(4.07, Math.Round(summary.GetColumnStats(2).Mean, 2));
      }

      [Fact]
      public void Check_datasetstats_numerical_sd()
      {
         var ds = new DataSet(new SchemaElement<string>("s"), new SchemaElement<int>("i"),
            new SchemaElement<float>("f")) { { "1", 2, 3.2F }, { "1", 3, 4F }, { "1", 4, 5F } };

         var summary = new DataSetSummaryStats(ds);
         Assert.Equal(1, summary.GetColumnStats(1).StandardDeviation);
         Assert.Equal(0.9, Math.Round(summary.GetColumnStats(2).StandardDeviation, 2));
      }

      [Fact]
      public void Check_datasetstats_numerical_nulls()
      {
         var ds = new DataSet(new SchemaElement<string>("s"), new SchemaElement<int>("i"),
            new SchemaElement<float>("f")) { { "1", 2, null }, { null, null, 4F }, { "1", 4, null } };

         var summary = new DataSetSummaryStats(ds);
         Assert.Equal(1, summary.GetColumnStats(0).NullCount);
         Assert.Equal(0, summary.GetColumnStats(0).Mean);
         Assert.Equal(1, summary.GetColumnStats(1).NullCount);
         Assert.Equal(2, summary.GetColumnStats(2).NullCount);
      }

      [Fact]
      public void Check_datasetstats_matrix_rows_cols()
      {
         var ds = new DataSet(new SchemaElement<double>("a"), new SchemaElement<double>("b"),
            new SchemaElement<double>("c")) { { 1.2, 1.5, 1.2 }, { 2.1, 2.2, 1.2 } };

         var matrix = new Matrix(ds);
         Assert.Equal(3, matrix.Cols);
         Assert.Equal(2, matrix.Rows);
      }

      [Fact]
      public void Check_datasetstats_matrix_covar_rowcheck()
      {
         var ds = new DataSet(new SchemaElement<double>("a"), new SchemaElement<double>("b"),
            new SchemaElement<double>("c")) { { 1.2, 1.5, 1.2 }, { 2.1, 2.2, 1.2 }, { 2.178, 2.212, 1.27778 }, { 2.178, 2.212, 1.27778 } };

         var matrix = Matrix.GetCovarianceMatrix(ds);
         Assert.Equal(3, matrix.Cols);
         Assert.Equal(3, matrix.Rows);
      }

      [Fact]
      public void Check_datasetstats_matrix_cov_equality()
      {
         var ds = new DataSet(
            new SchemaElement<double>("x"), 
            new SchemaElement<double>("y")) { { 1.0, 1.0 }, { 2.0, 2.0 }, { 3.0, 3.0 } };

         var matrix = Matrix.GetCorrelationMatrix(ds);
         Assert.Equal(matrix[1, 0], matrix[0, 1]);
      }

      [Fact]
      public void Check_datasetstats_matrix_cor_unity()
      {
         var ds = new DataSet(
            new SchemaElement<double>("x"),
            new SchemaElement<double>("y")) { { 1.0, 4.0 }, { 2.0, 7.0 }, { 3.0, 9.0 } };

         var matrix = Matrix.GetCorrelationMatrix(ds);
         Assert.Equal(1, matrix[0, 0]);
         Assert.Equal(0.9934, Math.Round(matrix[0, 1], 4));
         Assert.Equal(1, matrix[1, 1]);
      }

      [Fact]
      public void Check_datasetstats_interquartilerange()
      {
         var ds = new DataSet(
            new SchemaElement<double>("x"))
            { {1D},{3D},{4d},{6d},{7d},{10d},{19d},{87d}};

         var summary = new DataSetSummaryStats(ds);
         Assert.Equal(3.5, summary.GetColumnStats(0).Quartile25);
         Assert.Equal(6.5, summary.GetColumnStats(0).Median);
         Assert.Equal(14.5, summary.GetColumnStats(0).Quartile75);
      }

      [Fact]
      public void Check_datasetstats_skewness()
      {
         var ds = new DataSet(
               new SchemaElement<double>("y"))
            { {5D},{20D},{40d},{80d},{100d},{102d} };

         var summary = new DataSetSummaryStats(ds);
         Assert.Equal(-0.096, Math.Round(summary.GetColumnStats(0).Skewness, 3));
      }

      [Fact]
      public void Check_datasetstats_kurtosis()
      {
         var ds = new DataSet(
               new SchemaElement<double>("y"))
            { {5D},{20D},{40d},{80d},{100d},{102d} };

         var summary = new DataSetSummaryStats(ds);
         Assert.Equal(1.341, Math.Round(summary.GetColumnStats(0).Kutosis, 3));
      }
   }
}
