using System;
using System.Collections.Specialized;
using System.Linq;
using ByteDev.Collections;
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
                var sut = new Uri("http://local/mypath");

                var result = sut.AppendPath(path);

                Assert.That(result, Is.EqualTo(new Uri("http://local/mypath/" + path)));
            }

            [TestCase("/path")]
            [TestCase("/path/")]
            [TestCase("/path/path2")]
            [TestCase("/path/path2/")]
            public void WhenHasPath_AndStartsWithSlash_ThenAppendPath(string path)
            {
                var sut = new Uri("http://local/mypath/");

                var result = sut.AppendPath(path);
                
                Assert.That(result, Is.EqualTo(new Uri("http://local/mypath" + path)));
            }
        }

        [TestFixture]
        public class QueryToDictionary
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => UriExtensions.QueryToDictionary(null));
            }

            [TestCase("http://www.somewhere.com/")]
            [TestCase("http://www.somewhere.com/app")]
            [TestCase("http://www.somewhere.com/app?")]
            public void WhenHasNoQueryString_ThenReturnEmpty(string uri)
            {
                var sut = new Uri(uri);

                var result = sut.QueryToDictionary();

                Assert.That(result, Is.Empty);
            }

            [Test]
            public void WhenHasOneParamAndValue_ThenReturnOneNameValuePair()
            {
                var sut = new Uri("http://www.somewhere.com/?s=hello");

                var result = sut.QueryToDictionary();

                Assert.That(result.Single().Key, Is.EqualTo("s"));
                Assert.That(result.Single().Value, Is.EqualTo("hello"));
            }

            [Test]
            public void WhenHasTwoParamsAndValues_ThenReturnTwoNameValuePairs()
            {
                var sut = new Uri("http://www.somewhere.com/?s=hello&w=WORLD");

                var result = sut.QueryToDictionary();

                Assert.That(result.Count, Is.EqualTo(2));
                Assert.That(result.First().Key, Is.EqualTo("s"));
                Assert.That(result.First().Value, Is.EqualTo("hello"));
                Assert.That(result.Second().Key, Is.EqualTo("w"));
                Assert.That(result.Second().Value, Is.EqualTo("WORLD"));
            }

            [Test]
            public void WhenHasParamWithNoValue_ThenReturnOneValuePair()
            {
                var sut = new Uri("http://www.somewhere.com/?s=");

                var result = sut.QueryToDictionary();

                Assert.That(result.Single().Key, Is.EqualTo("s"));
                Assert.That(result.Single().Value, Is.EqualTo(string.Empty));
            }

            [Test]
            public void WhenHasTwoParamsWithNoValues_ThenReturnTwoNameValuePairs()
            {
                var sut = new Uri("http://www.somewhere.com/?s=&w=");

                var result = sut.QueryToDictionary();

                Assert.That(result.Count, Is.EqualTo(2));
                Assert.That(result.First().Key, Is.EqualTo("s"));
                Assert.That(result.First().Value, Is.EqualTo(string.Empty));
                Assert.That(result.Second().Key, Is.EqualTo("w"));
                Assert.That(result.Second().Value, Is.EqualTo(string.Empty));
            }
        }
        
        [TestFixture]
        public class QueryToNameValueCollection
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => UriExtensions.QueryToNameValueCollection(null));
            }

            [TestCase("http://www.somewhere.com/")]
            [TestCase("http://www.somewhere.com/app")]
            [TestCase("http://www.somewhere.com/app?")]
            public void WhenHasNoQueryString_ThenReturnEmpty(string uri)
            {
                var sut = new Uri(uri);

                var result = sut.QueryToNameValueCollection();

                Assert.That(result, Is.Empty);
            }

            [Test]
            public void WhenHasOneParamAndValue_ThenReturnOneNameValuePair()
            {
                var sut = new Uri("http://www.somewhere.com/?s=hello");

                var result = sut.QueryToNameValueCollection();

                Assert.That(result.AllKeys.Length, Is.EqualTo(1));
                Assert.That(result.GetValues("s")?.Single(), Is.EqualTo("hello"));
            }

            [Test]
            public void WhenHasTwoParamsAndValues_ThenReturnTwoNameValuePairs()
            {
                var sut = new Uri("http://www.somewhere.com/?s=hello&w=WORLD");

                var result = sut.QueryToNameValueCollection();

                Assert.That(result.AllKeys.Length, Is.EqualTo(2));
                Assert.That(result.GetValues("s")?.Single(), Is.EqualTo("hello"));
                Assert.That(result.GetValues("w")?.Single(), Is.EqualTo("WORLD"));
            }

            [Test]
            public void WhenHasTwoParamsWithSameName_ThenReturnOneNameTwoValues()
            {
                var sut = new Uri("http://www.somewhere.com/?s=hello&s=world");

                var result = sut.QueryToNameValueCollection();

                Assert.That(result.AllKeys.Length, Is.EqualTo(1));
                Assert.That(result.GetValues("s")?.First(), Is.EqualTo("hello"));
                Assert.That(result.GetValues("s")?.Second(), Is.EqualTo("world"));
            }

            [Test]
            public void WhenHasParamWithNoValue_ThenReturnOneValuePair()
            {
                var sut = new Uri("http://www.somewhere.com/?s=");

                var result = sut.QueryToNameValueCollection();

                Assert.That(result.AllKeys.Length, Is.EqualTo(1));
                Assert.That(result.GetValues("s")?.Single(), Is.Empty);
            }

            [Test]
            public void WhenHasTwoParamsWithNoValues_ThenReturnTwoNameValuePairs()
            {
                var sut = new Uri("http://www.somewhere.com/?s=&w=");

                var result = sut.QueryToNameValueCollection();

                Assert.That(result.AllKeys.Length, Is.EqualTo(2));
                Assert.That(result.GetValues("s")?.Single(), Is.Empty);
                Assert.That(result.GetValues("w")?.Single(), Is.Empty);
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
                var expected = new Uri("http://localhost/myapp");

                var sut = new Uri("http://localhost/myapp?name=value");

                var result = sut.AddOrUpdateQueryParam("name", null);

                Assert.That(result, Is.EqualTo(expected));
            }

            [TestCase("")]
            [TestCase("value2")]
            public void WhenParamExists_AndValueIsNotNull_ThenUpdateParamValue(string value)
            {
                var expected = new Uri($"http://localhost/myapp?name={value}");

                var sut = new Uri("http://localhost/myapp?name=value");

                var result = sut.AddOrUpdateQueryParam("name", value);
                
                Assert.That(result, Is.EqualTo(expected));                
            }

            [Test]
            public void WhenParamNotExists_AndValueIsNull_ThenNotRemoveParam()
            {
                var expected = new Uri("http://localhost/myapp");

                var sut = new Uri("http://localhost/myapp");

                var result = sut.AddOrUpdateQueryParam("name", null);

                Assert.That(result, Is.EqualTo(expected));
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
        }

        [TestFixture]
        public class AddOrUpdateQueryParams
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

                Assert.Throws<ArgumentNullException>(() => sut.AddOrUpdateQueryParams(null));
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
    }
}
