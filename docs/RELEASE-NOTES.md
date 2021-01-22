# Release Notes

## 1.2.0 - ?

Breaking changes:
- (None)

New features:
- Added `UriSetExtensions.SetQuery` overload for `IEnumerable<string>`.

Bug fixes / internal changes:
- Fixed `UriQueryConverter.ToNameValueCollection` implementation to handle query string name params with no value.
- Fixed `UriQueryConverter.ToDictionary` implementation to handle query string name params with no value.

## 1.1.0 - 11 December 2020

Breaking changes:
- (None)

New features:
- Added overload for `UriQueryConverter.ToString`.

Bug fixes / internal changes:
- (None)

## 1.0.0 - 30 July 2020

Initial version.
