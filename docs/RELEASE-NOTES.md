# Release Notes

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
