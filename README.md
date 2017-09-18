# DataFrames [![Build status](https://ci.appveyor.com/api/projects/status/dg4lwtd6mq8w2gfr/branch/master?svg=true)](https://ci.appveyor.com/project/aloneguid/dataframe-utils/branch/master)

A library consuming partquet-dotnet used to build a feature abstractions for ParquetReader. 
The following are features that are available through the library.

- Ability to scale page reads across Service Fabric nodes
- Execute correlation matrix across large parquet DataSet
- Summary column statistics across millions or billions of rows
- Linear regression at Service Fabric scale with SGD

This project exposes a few packages:

## DataFrame.Math

A set of LINQ operators to calculate the following functions on a stream of numbers:

- Kurtosis
- Mean
- Quantile25, Quantile75
- Median
- Skewness
- Standard Deviation
- Variance

## Contributing

Please read [contibuting](doc/contributing.md) section for general git guidanec and [architecture](doc/architecture.md) for code orientation.