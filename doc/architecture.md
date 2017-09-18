# Architecture choices

This document is provided for guidance only and can always be changed or amended by you.

## Adding a statistical function

Functions are located within **src/DataFrame.Math** project under **Operators** folder. All the functions are implemented as LINQ operators extending `IReadOnlyCollection<double>`. This is the topmost interface allowing you to both enumerate the elements and get the count of them.

The reasons for using LINQ operators is because of ease of use in C# calculations without having to set up heavy initialisation code. Please look at the existing examples and try to be a ninja to fit in with an existing style.

![](ninja.png)

## Code formatting etc.

This project uses the [coding style](https://github.com/dotnet/corefx/blob/master/Documentation/coding-guidelines/coding-style.md) from the [dotnet/corefx](https://github.com/dotnet/corefx) repo.

Some of the rules are enforced by `.editorconfig` file recognized by Visual Studio 2017.