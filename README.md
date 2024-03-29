[![Build status](https://ci.appveyor.com/api/projects/status/github/bytedev/ByteDev.ResourceIdentifier?branch=master&svg=true)](https://ci.appveyor.com/project/bytedev/ByteDev-ResourceIdentifier/branch/master)
[![NuGet Package](https://img.shields.io/nuget/v/ByteDev.ResourceIdentifier.svg)](https://www.nuget.org/packages/ByteDev.ResourceIdentifier)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://github.com/ByteDev/ByteDev.ResourceIdentifier/blob/master/LICENSE)

# ByteDev.ResourceIdentifier

.NET Standard library of resource identifier (URI) related functionality.

## Installation

ByteDev.ResourceIdentifier has been written as a .NET Standard 2.0 library, so you can consume it from a .NET Core or .NET Framework 4.6.1 (or greater) application.

ByteDev.ResourceIdentifier is hosted as a package on nuget.org.  To install from the Package Manager Console in Visual Studio run:

`Install-Package ByteDev.ResourceIdentifier`

Further details can be found on the [nuget page](https://www.nuget.org/packages/ByteDev.ResourceIdentifier/).

## Release Notes

Releases follow semantic versioning.

Full details of the release notes can be viewed on [GitHub](https://github.com/ByteDev/ByteDev.ResourceIdentifier/blob/master/docs/RELEASE-NOTES.md).

## Usage

`Uri` extension methods:

- AddOrUpdateQueryParam
- AddOrUpdateQueryParams
- AppendPath
- GetPathSegments
- GetRoot
- HasFragment
- HasPath
- HasQuery
- QueryToDictionary
- QueryToNameValueCollection
- RemoveFragment
- RemovePath
- RemoveQuery
- RemoveQueryParam
- RemoveQueryParams
- SetFragment
- SetPath
- SetPort
- SetPortDefault
- SetQuery
- SetScheme

Example:

```csharp
var uri = new Uri("https://example.com/")
    .AppendPath("over")
    .AppendPath("there")
    .AddOrUpdateQueryParam("name", "John")
    .SetFragment("myfrag");

// uri.AbsoluteUri = "https://example.com/over/there?name=John#myfrag"
```

---

### UriQueryConverter

The `UriQueryConverter` class can be used to convert a URI query to various different formats.

Methods:

- ToString
- ToDictionary
- ToNameValueCollection

Example:

```csharp
var nameValues = new NameValueCollection
{
    {"key1", "value1"},
    {"key2", "value2"}
};

var query = UriQueryConverter.ToString(nameValues);

// query == "?key1=value1&key2=value2"
```

---

### UriSlugBuilder

The `UriSlugBuilder` class can be used to create *slugs* for use in URI paths.

Example:

```csharp
string slug = new UriSlugBuilder()
                .WithText("My First Blog Post")
                .WithDateTimeSuffix(new DateTime(2022, 6, 1))
                .Build();

// slug == "my-first-blog-post-MjAyMjA2MDExMjAwMDA"

var uri = new Uri("http://localhost/myblog")
                .AppendPath(slug);

// uri == new Uri("http://localhost/myblog/my-first-blog-post-MjAyMjA2MDExMjAwMDA")
```
