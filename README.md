[![Build status](https://ci.appveyor.com/api/projects/status/github/bytedev/ByteDev.ResourceIdentifier?branch=master&svg=true)](https://ci.appveyor.com/project/bytedev/ByteDev-ResourceIdentifier/branch/master)
[![NuGet Package](https://img.shields.io/nuget/v/ByteDev.ResourceIdentifier.svg)](https://www.nuget.org/packages/ByteDev.ResourceIdentifier)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://github.com/ByteDev/ByteDev.ResourceIdentifier/blob/master/LICENSE)

# ByteDev.ResourceIdentifier

.NET Standard library of resource identifer (URI) related functionality.

## Installation

ByteDev.ResourceIdentifier has been written as a .NET Standard 2.0 library, so you can consume it from a .NET Core or .NET Framework 4.6.1 (or greater) application.

ByteDev.ResourceIdentifier is hosted as a package on nuget.org.  To install from the Package Manager Console in Visual Studio run:

`Install-Package ByteDev.ResourceIdentifier`

Further details can be found on the [nuget page](https://www.nuget.org/packages/ByteDev.ResourceIdentifier/).

## Release Notes

Releases follow semantic versioning.

Full details of the release notes can be viewed on [GitHub](https://github.com/ByteDev/ByteDev.ResourceIdentifier/blob/master/docs/RELEASE-NOTES.md).

## Usage

Uri extension methods:

- AddOrUpdateQueryParam
- AddOrUpdateQueryParams
- AppendPath
- QueryToDictionary
- QueryToNameValueCollection
- RemoveQuery
- RemoveQueryParam
- HasPath
- HasQuery
- HasFragment
- SetPath
- SetQuery

Example:

```csharp
var uri = new Uri("https://example.com/")
    .AppendPath("over")
    .AppendPath("there")
    .AddOrUpdateQueryParam("name", "John")
    .SetFragment("myfrag");

Console.Write(uri.AbsoluteUri);  // "https://example.com/over/there?name=John#myfrag"
```

UriQueryConverter class methods:

- ToString
- ToNameValueCollection