using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    }
}
