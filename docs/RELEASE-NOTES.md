# Release Notes

## 2.2.0 - ?

Breaking changes:
- (None)

New features:
- Added `Uri.RemovePath` extension method.

Bug fixes / internal changes:
- Fix in `UriQueryConverter.ToString(NameValueCollection)` to better handle nulls and empty strings.
- Fix in `UriQueryConverter.ToString(Dictionary)` to better handle nulls and empty strings.
- Fix in `UriQueryConverter.ToString(IEnumerable)` to better handle nulls and empty strings.

## 2.1.1 - 24 July 2021

Breaking changes:
- (None)

New features:
- (None)

Bug fixes / internal changes:
- Fix where `SetQuery` method would add extra query string question mark prefix when consumed in .NET Framework.
- Fix where `SetFragment` method would add extra fragment hash prefix when consumed in .NET Framework.

## 2.1.0 - 24 July 2021

Breaking changes:
- (None)

New features:
- Added `AppendPath` overload that takes segments.

Bug fixes / internal changes:
- Fixed bug in `AppendPath` method where path not always appended correctly if URI has querystring, fragment etc.

## 2.0.0 - 23 January 2021

Breaking changes:
- `Uri` extension method `HasPath` now returns false instead of throwing exception if `Uri` is null.
- `Uri` extension method `HasQuery` now returns false instead of throwing exception  if `Uri` is null.
- `Uri` extension method `HasFragment` now returns false instead of throwing exception if `Uri` is null.
- `Uri` extension method `RemoveQuery` now returns null instead of throwing exception if `Uri` is null.
- `Uri` extension method `RemoveQueryParam` now returns null instead of throwing exception if `Uri` is null.
- `Uri` extension method `RemoveQueryParams` now returns null instead of throwing exception if `Uri` is null.
- `Uri` extension method `RemoveFragment` now returns null instead of throwing exception if `Uri` is null.
- `Uri` extension method `QueryToDictionary` now returns empty instead of throwing exception if `Uri` is null.
- `Uri` extension method `QueryToNameValueCollection` now returns empty instead of throwing exception if `Uri` is null.

New features:
- Added `Uri` extension method overload on `SetQuery` for `IEnumerable<string>`.
- Added `Uri` extension method overload on `AddOrUpdateQueryParams` for `IDictionary<string, string>`.

Bug fixes / internal changes:
- Fixed `UriQueryConverter.ToNameValueCollection` implementation to handle query string name params with no value.
- Fixed `UriQueryConverter.ToDictionary` implementation to handle query string name params with no value.
- Fixed `UriQueryConverter` to always do appropriate Url encode/decode on names and values.

## 1.1.0 - 11 December 2020

Breaking changes:
- (None)

New features:
- Added overload for `UriQueryConverter.ToString` for `IEnumerable<string>`.

Bug fixes / internal changes:
- (None)

## 1.0.0 - 30 July 2020

Initial version.
