using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using ByteDev.Collections;
using ByteDev.Strings;
using NUnit.Framework;

namespace ByteDev.ResourceIdentifier.UnitTests
{
    [TestFixture]
    public class UriExtensionsTests
    {
        [TestFixture]
        public class AppendPath
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => UriExtensions.AppendPath(null, "path"));
            }

            [TestCase(null)]
            [TestCase("")]
            public void WhenPathIsNullOrEmpty_ThenReturnSameUri(string path)
            {
                var sut = new Uri("http://local/myapp");

                var result = sut.AppendPath(path);

                Assert.That(sut, Is.SameAs(result));
            }

            [TestCase("path")]
            [TestCase("path/")]
            [TestCase("path/path2")]
            [TestCase("path/path2/")]
            public void WhenHasNoPath_ThenAppendPath(string path)
            {
                var sut = new Uri("http://local/");

                var result = sut.AppendPath(path);
                
                Assert.That(result, Is.EqualTo(new Uri("http://local/" + path)));
            }

            [TestCase("/path")]
            [TestCase("/path/")]
            [TestCase("/path/path2")]
            [TestCase("/path/path2/")]
            public void WhenHasNoPath_AndStartsWithSlash_ThenAppendPath(string path)
            {
                var sut = new Uri("http://local/");

                var result = sut.AppendPath(path);
                
                Assert.That(result, Is.EqualTo(new Uri("http://local" + path)));
            }

            [TestCase("path")]
            [TestCase("path/")]
            [TestCase("path/path2")]
            [TestCase("path/path2/")]
            public void WhenHasPath_ThenAppendPath(string path)
            {
                var sut = new Uri("https://example.com:8080/over/there?name=John#myfrag");

                var result = sut.AppendPath(path);

                Assert.That(result, Is.EqualTo(new Uri($"https://example.com:8080/over/there/{path}?name=John#myfrag")));
            }

            [TestCase("/path")]
            [TestCase("/path/")]
            [TestCase("/path/path2")]
            [TestCase("/path/path2/")]
            public void WhenHasPath_AndStartsWithSlash_ThenAppendPath(string path)
            {
                var sut = new Uri("https://example.com:8080/over/there?name=John#myfrag");

                var result = sut.AppendPath(path);

                var pathNoStartSlash = path.RemoveStartsWith("/");

                Assert.That(result, Is.EqualTo(new Uri($"https://example.com:8080/over/there/{pathNoStartSlash}?name=John#myfrag")));
            }

            [Test]
            public void WhenPathHasEndSlash_ThenAppendPath()
            {
                var sut = new Uri("https://example.com:8080/over/there/?name=John#myfrag");

                var result = sut.AppendPath("path/path2");
                
                Assert.That(result, Is.EqualTo(new Uri("https://example.com:8080/over/there/path/path2/?name=John#myfrag")));
            }
        }

        [TestFixture]
        public class AppendPath_Segments
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => UriExtensions.AppendPath(null, "path", "path2"));
            }

            [Test]
            public void WhenSegmentsIsNull_ThenReturnSameUri()
            {
                var sut = new Uri("http://local");

                var result = sut.AppendPath(null as string[]);

                Assert.That(result, Is.SameAs(sut));
            }

            [Test]
            public void WhenSegmentsIsEmpty_ThenReturnSameUri()
            {
                var sut = new Uri("http://local");

                var result = sut.AppendPath();

                Assert.That(result, Is.SameAs(sut));
            }

            [TestCase("path")]
            [TestCase("/path")]
            [TestCase("/path/")]
            public void WhenOneSegment_ThenAppendPath(string segment)
            {
                var sut = new Uri("https://example.com:8080/over/there?name=John#myfrag");

                var result = sut.AppendPath(new[] { segment });

                Assert.That(result, Is.EqualTo(new Uri("https://example.com:8080/over/there/path?name=John#myfrag")));
            }

            [TestCase("path1", "path2")]
            [TestCase("/path1", "/path2")]
            [TestCase("/path1/", "/path2/")]
            public void WhenTwoSegments_ThenAppendPath(string segment1, string segment2)
            {
                var sut = new Uri("https://example.com:8080/over/there?name=John#myfrag");

                var result = sut.AppendPath(segment1, segment2);

                Assert.That(result, Is.EqualTo(new Uri("https://example.com:8080/over/there/path1/path2?name=John#myfrag")));
            }

            [Test]
            public void WhenHasNoPath_ThenAppendPath()
            {
                var sut = new Uri("https://example.com:8080/?name=John#myfrag");

                var result = sut.AppendPath("path1", "path2");

                Assert.That(result, Is.EqualTo(new Uri("https://example.com:8080/path1/path2?name=John#myfrag")));
            }
        }

        [TestFixture]
        public class AddOrUpdateQueryParam
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => UriExtensions.AddOrUpdateQueryParam(null, "name", "value"));
            }
            
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullorEmpty_ThenThrowException(string name)
            {
                var sut = new Uri("http://localhost/myapp");

                Assert.Throws<ArgumentException>(() => sut.AddOrUpdateQueryParam(name, "value"));
            }
            
            [Test]
            public void WhenParamExists_AndValueIsNull_ThenRemoveParam()
            {
                var sut = new Uri("http://localhost/myapp?name=value");

                var result = sut.AddOrUpdateQueryParam("name", null);

                Assert.That(result, Is.EqualTo(new Uri("http://localhost/myapp")));
            }

            [TestCase("")]
            [TestCase("value2")]
            public void WhenParamExists_AndValueIsNotNull_ThenUpdateParamValue(string value)
            {
                var sut = new Uri("http://localhost/myapp?name=value");

                var result = sut.AddOrUpdateQueryParam("name", value);
                
                Assert.That(result, Is.EqualTo(new Uri($"http://localhost/myapp?name={value}")));                
            }

            [TestCase("http://localhost/")]
            [TestCase("http://localhost/?apikey=ABC123")]
            [TestCase("http://localhost/myapp")]
            [TestCase("http://localhost/myapp?apikey=ABC123")]
            [TestCase("http://localhost/myapp/")]
            [TestCase("http://localhost/myapp/?apikey=ABC123")]
            public void WhenParamNotExists_AndValueIsNull_ThenNotRemoveParam(string uri)
            {
                var sut = new Uri(uri);

                var result = sut.AddOrUpdateQueryParam("name", null);

                Assert.That(result, Is.EqualTo(new Uri(uri)));
            }

            [TestCase("")]
            [TestCase("value")]
            public void WhenParamNotExists_AndValueIsNotNull_ThenAddParam(string value)
            {
                var sut = new Uri("http://localhost/myapp");

                var result = sut.AddOrUpdateQueryParam("name", value);

                Assert.That(result, Is.EqualTo(new Uri($"http://localhost/myapp?name={value}")));                
            }

            [Test]
            public void WhenValueContainsSpace_ThenEncodeValue()
            {
                var sut = new Uri("http://localhost/myapp");

                var result = sut.AddOrUpdateQueryParam("name", "John Smith");
                
                Assert.That(result.AbsoluteUri, Is.EqualTo("http://localhost/myapp?name=John+Smith"));
            }

            [Test]
            public void WhenHasNoQueryString_AndNoTrailingSlash_ThenAddParam()
            {
                var sut = new Uri("http://api.giphy.com/v1/gifs/search");

                var result = sut.AddOrUpdateQueryParam("api_key", "ABC123");

                Assert.That(result, Is.EqualTo(new Uri("http://api.giphy.com/v1/gifs/search?api_key=ABC123")));
            }

            [Test]
            public void WhenHasNoQueryString_AndTrailingSlash_ThenAddParam()
            {
                var sut = new Uri("http://api.giphy.com/v1/gifs/search/");

                var result = sut.AddOrUpdateQueryParam("api_key", "ABC123");

                Assert.That(result, Is.EqualTo(new Uri("http://api.giphy.com/v1/gifs/search/?api_key=ABC123")));
            }
        }

        [TestFixture]
        public class AddOrUpdateQueryParams_NameValueCollection
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => UriExtensions.AddOrUpdateQueryParams(null, new NameValueCollection()));
            }

            [Test]
            public void WhenNameValuesIsNull_ThenThrowException()
            {
                var sut = new Uri("http://localhost/myapp");

                Assert.Throws<ArgumentNullException>(() => sut.AddOrUpdateQueryParams(null as NameValueCollection));
            }

            [Test]
            public void WhenNameValuesIsEmpty_ThenReturnEqual()
            {
                var sut = new Uri("http://localhost/myapp?q1=1&q2=2");

                var result = sut.AddOrUpdateQueryParams(new NameValueCollection());

                Assert.That(result, Is.EqualTo(sut));
                Assert.That(result, Is.Not.SameAs(sut));
            }

            [Test]
            public void WhenNameValuesHasItems_ThenUpdateQuery()
            {
                var sut = new Uri("http://localhost/myapp?q1=1&q2=2");

                var result = sut.AddOrUpdateQueryParams(new NameValueCollection
                {
                    { "q2", "3" },
                    { "q3", "10" }
                });

                Assert.That(result, Is.EqualTo(new Uri("http://localhost/myapp?q1=1&q2=3&q3=10")));
                Assert.That(result, Is.Not.SameAs(sut));
            }
        }

        [TestFixture]
        public class AddOrUpdateQueryParams_Dictionary
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => UriExtensions.AddOrUpdateQueryParams(null, new Dictionary<string, string>()));
            }

            [Test]
            public void WhenNameValuesIsNull_ThenThrowException()
            {
                var sut = new Uri("http://localhost/myapp");

                Assert.Throws<ArgumentNullException>(() => sut.AddOrUpdateQueryParams(null as IDictionary<string, string>));
            }

            [Test]
            public void WhenNameValuesIsEmpty_ThenReturnEqual()
            {
                var sut = new Uri("http://localhost/myapp?q1=1&q2=2");

                var result = sut.AddOrUpdateQueryParams(new Dictionary<string, string>());

                Assert.That(result, Is.EqualTo(sut));
                Assert.That(result, Is.Not.SameAs(sut));
            }

            [Test]
            public void WhenNameValuesHasItems_ThenUpdateQuery()
            {
                var sut = new Uri("http://localhost/myapp?q1=1&q2=2");

                var result = sut.AddOrUpdateQueryParams(new Dictionary<string, string>()
                {
                    { "q2", "3" },
                    { "q3", "10" }
                });

                Assert.That(result, Is.EqualTo(new Uri("http://localhost/myapp?q1=1&q2=3&q3=10")));
                Assert.That(result, Is.Not.SameAs(sut));
            }
        }

        [TestFixture]
        public class GetPathSegments
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => UriExtensions.GetPathSegments(null));
            }

            [TestCase("http://localhost")]
            [TestCase("http://localhost/")]
            public void WhenUriHasNoPath_ThenReturnEmpty(string uri)
            {
                var sut = new Uri(uri);

                var result = sut.GetPathSegments();

                Assert.That(result, Is.Empty);
            }

            [TestCase("http://localhost/path1")]
            [TestCase("http://localhost/path1/")]
            [TestCase("http://localhost/path1?name=John")]
            [TestCase("http://localhost/path1/?name=John")]
            public void WhenUriHasOnePathSegment_ThenReturnSegment(string uri)
            {
                var sut = new Uri(uri);

                var result = sut.GetPathSegments();

                Assert.That(result.Single(), Is.EqualTo("path1"));
            }

            [TestCase("http://localhost/path1/path2")]
            [TestCase("http://localhost/path1/path2/")]
            [TestCase("http://localhost/path1/path2?name=John")]
            [TestCase("http://localhost/path1/path2/?name=John")]
            public void WhenUriHasTwoPathSegments_ThenReturnSegments(string uri)
            {
                var sut = new Uri(uri);

                var result = sut.GetPathSegments();

                Assert.That(result.Count(), Is.EqualTo(2));
                Assert.That(result.First(), Is.EqualTo("path1"));
                Assert.That(result.Second(), Is.EqualTo("path2"));
            }
        }

        [TestFixture]
        public class GetRoot
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => UriExtensions.GetRoot(null));
            }

            [TestCase("http://something.com/")]
            [TestCase("http://something.com/path1")]
            [TestCase("http://something.com/path1/")]
            [TestCase("http://something.com/path1/path2")]
            [TestCase("http://something.com/path1/path2/")]
            [TestCase("http://something.com/path1/path2?name=John")]
            public void WhenHasNoPort_ThenReturnRoot(string uri)
            {
                var sut = new Uri(uri);

                var result = sut.GetRoot();

                Assert.That(result, Is.EqualTo("http://something.com"));
            }

            [Test]
            public void WhenIsHttp_AndHasNonDefaultPort_ThenReturnRootWithPort()
            {
                var sut = new Uri("http://something.com:8080/path1");

                var result = sut.GetRoot();

                Assert.That(result, Is.EqualTo("http://something.com:8080"));
            }

            [Test]
            public void WhenIsHttps_AndHasNonDefaultPort_ThenReturnRootWithPort()
            {
                var sut = new Uri("https://something.com:8080/path1");

                var result = sut.GetRoot();

                Assert.That(result, Is.EqualTo("https://something.com:8080"));
            }

            [TestCase("http://something.com/path1")]
            [TestCase("http://something.com:80/path1")]
            public void WhenIsHttp_AndDefaultPort_ThenReturnRootWithoutPort(string uri)
            {
                var sut = new Uri(uri);

                var result = sut.GetRoot();

                Assert.That(result, Is.EqualTo("http://something.com"));
            }

            [TestCase("https://something.com/path1")]
            [TestCase("https://something.com:443/path1")]
            public void WhenIsHttps_AndDefaultPort_ThenReturnRootWithoutPort(string uri)
            {
                var sut = new Uri(uri);

                var result = sut.GetRoot();

                Assert.That(result, Is.EqualTo("https://something.com"));
            }
        }
    }
}
