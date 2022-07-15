using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using NUnit.Framework;

namespace ByteDev.ResourceIdentifier.UnitTests
{
    [TestFixture]
    public class UriSetExtensionsTests
    {
        [TestFixture]
        public class SetScheme
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => UriSetExtensions.SetScheme(null, "https"));
            }

            [TestCase(null)]
            [TestCase("")]
            public void WhenSchemeIsNullOrEmpty_ThenThrowException(string scheme)
            {
                var sut = new Uri("http://localhost/");

                Assert.Throws<ArgumentException>(() => sut.SetScheme(scheme));
            }

            [Test]
            public void WhenSchemeIsValid_AndPortNotSet_ThenSetScheme()
            {
                var sut = new Uri("http://localhost/");

                var result = sut.SetScheme(Uri.UriSchemeHttps);

                Assert.That(result.AbsoluteUri, Is.EqualTo("https://localhost/"));
            }

            [Test]
            public void WhenSchemeIsValid_AndPortSet_ThenSetScheme()
            {
                var sut = new Uri("http://localhost:8080/");

                var result = sut.SetScheme(Uri.UriSchemeHttps);

                Assert.That(result.AbsoluteUri, Is.EqualTo("https://localhost:8080/"));
            }
        }

        [TestFixture]
        public class SetPort
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => UriSetExtensions.SetPort(null, 8080));
            }

            [TestCase(-1)]
            [TestCase(65536)]
            public void WhenPortIsOutOfRange_ThenThrowException(int port)
            {
                var sut = new Uri("http://localhost/");

                Assert.Throws<ArgumentOutOfRangeException>(() => sut.SetPort(port));
            }
            
            [TestCase(0)]
            [TestCase(8080)]
            [TestCase(65535)]
            public void WhenPortIsNotDefault_ThenSetPort(int port)
            {
                var sut = new Uri("http://localhost/");

                var result = sut.SetPort(port);

                Assert.That(result.AbsoluteUri, Is.EqualTo($"http://localhost:{port}/"));
            }

            [Test]
            public void WhenPortIsDefault_ThenPortDoesNotChange()
            {
                var sut = new Uri("http://localhost/");

                var result = sut.SetPort(80);

                Assert.That(result.AbsoluteUri, Is.EqualTo("http://localhost/"));
            }
        }

        [TestFixture]
        public class SetPortDefault
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => UriSetExtensions.SetPortDefault(null));
            }

            [Test]
            public void WhenPortIsNotDefault_ThenSetDefaultPort()
            {
                var sut = new Uri("http://localhost:8080/");

                var result = sut.SetPortDefault();

                Assert.That(result.AbsoluteUri, Is.EqualTo("http://localhost/"));
            }
        }

        [TestFixture]
        public class SetPath
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => UriSetExtensions.SetPath(null, "path"));
            }

            [TestCase(null)]
            [TestCase("")]
            [TestCase("/")]
            public void WhenPathIsNullOrEmptyOrSlash_ThenRemovePath(string path)
            {
                var sut = new Uri("http://localhost/path?name=value");

                var result = sut.SetPath(path);

                Assert.That(result, Is.EqualTo(new Uri("http://localhost/?name=value")));
            }

            [TestCase("newpath", "http://localhost/newpath")]
            [TestCase("/newpath", "http://localhost/newpath")]
            [TestCase("newpath/", "http://localhost/newpath/")]
            [TestCase("/newpath/", "http://localhost/newpath/")]
            public void WhenUriIsJustRoot_ThenSetPath(string path, string expected)
            {
                var sut = new Uri("http://localhost");

                var result = sut.SetPath(path);

                Assert.That(result, Is.EqualTo(new Uri(expected)));
            }

            [TestCase("newpath", "http://localhost/newpath?name=value")]
            [TestCase("/newpath", "http://localhost/newpath?name=value")]
            [TestCase("newpath/", "http://localhost/newpath/?name=value")]
            [TestCase("/newpath/", "http://localhost/newpath/?name=value")]
            public void WhenUriHasPath_ThenUpdatePath(string path, string expected)
            {
                var sut = new Uri("http://localhost/oldpath?name=value");

                var result = sut.SetPath(path);

                Assert.That(result, Is.EqualTo(new Uri(expected)));
            }

            [TestCase("newpath", "http://localhost/newpath?name=value")]
            [TestCase("/newpath", "http://localhost/newpath?name=value")]
            [TestCase("newpath/", "http://localhost/newpath/?name=value")]
            [TestCase("/newpath/", "http://localhost/newpath/?name=value")]
            public void WhenUriHasNoPath_ThenAddPath(string path, string expected)
            {
                var sut = new Uri("http://localhost/?name=value");

                var result = sut.SetPath(path);

                Assert.That(result, Is.EqualTo(new Uri(expected)));
            }
        }

        [TestFixture]
        public class SetQuery
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => UriSetExtensions.SetQuery(null, "name=value"));
            }

            [TestCase(null)]
            [TestCase("")]
            public void WhenQueryIsNullOrEmpty_ThenRemoveQuery(string query)
            {
                var sut = new Uri("http://localhost/path?name=value");

                var result = sut.SetQuery(query);

                Assert.That(result, Is.EqualTo(new Uri("http://localhost/path")));
            }

            [TestCase("http://localhost/")]
            [TestCase("http://localhost/path")]
            [TestCase("http://localhost/path/")]
            public void WhenUriHasNoQuery_ThenAddQuery(string uri)
            {
                var sut = new Uri(uri);

                var result = sut.SetQuery("age=50&name=John");

                Assert.That(result, Is.EqualTo(new Uri(uri + "?age=50&name=John")));
            }

            [Test]
            public void WhenQueryHasNoQuestionMark_ThenSetQuery()
            {
                var sut = new Uri("http://localhost/path?name=value");

                var result = sut.SetQuery("age=50");

                Assert.That(result, Is.EqualTo(new Uri("http://localhost/path?age=50")));
            }

            [Test]
            public void WhenQueryHasQuestionMark_ThenSetQuery()
            {
                var sut = new Uri("http://localhost/path?name=value");

                var result = sut.SetQuery("?age=50");

                Assert.That(result, Is.EqualTo(new Uri("http://localhost/path?age=50")));
            }

            [Test]
            public void WhenQueryHasQuestionMark_AndUriAlreadyHasQuestionMark_ThenSetQuery()
            {
                var sut = new Uri("http://localhost/path?");

                var result = sut.SetQuery("?age=50");

                Assert.That(result, Is.EqualTo(new Uri("http://localhost/path?age=50")));
            }

            [Test]
            public void WhenQueryHasSpaces_ThenSetQuery()
            {
                var sut = new Uri("http://localhost/path?name=value");

                var result = sut.SetQuery("first name=John Smith");

                Assert.That(result.AbsoluteUri, Is.EqualTo("http://localhost/path?first%20name=John%20Smith"));
            }
        }

        [TestFixture]
        public class SetQuery_NameValueCollection : UriSetExtensionsTests
        {
            [Test]
            public void WhenNameValueCollectionQueryIsNull_ThenRemoveQuery()
            {
                var sut = new Uri("http://localhost/path?name=value");

                var result = sut.SetQuery(null as NameValueCollection);
                
                Assert.That(result.AbsoluteUri, Is.EqualTo("http://localhost/path"));
            }

            [Test]
            public void WhenNameValueCollectionQueryIsEmpty_ThenRemoveQuery()
            {
                var sut = new Uri("http://localhost/path?name=value");

                var result = sut.SetQuery(new NameValueCollection());
                
                Assert.That(result.AbsoluteUri, Is.EqualTo("http://localhost/path"));
            }

            [Test]
            public void WhenNameValueCollectionQueryHasItems_ThenSetQuery()
            {
                var nameValues = new NameValueCollection
                {
                    { "name1", "value1" },
                    { "name2", "value2" }
                };
                
                var sut = new Uri("http://localhost/path?name=value");

                var result = sut.SetQuery(nameValues);

                Assert.That(result.AbsoluteUri, Is.EqualTo("http://localhost/path?name1=value1&name2=value2"));
            }
        }

        [TestFixture]
        public class SetQuery_Enumerable : UriSetExtensionsTests
        {
            [Test]
            public void WhenEnumerableQueryIsNull_ThenRemoveQuery()
            {
                var sut = new Uri("http://localhost/path?name=value");

                var result = sut.SetQuery(null as IEnumerable<string>);
                
                Assert.That(result.AbsoluteUri, Is.EqualTo("http://localhost/path"));
            }

            [Test]
            public void WhenEnumerableQueryIsEmpty_ThenRemoveQuery()
            {
                var sut = new Uri("http://localhost/path?name=value");

                var result = sut.SetQuery(Enumerable.Empty<string>());
                
                Assert.That(result.AbsoluteUri, Is.EqualTo("http://localhost/path"));
            }

            [Test]
            public void WhenEnumerableQueryHasItems_ThenSetQuery()
            {
                var names = new[] { "name1", "name2" };

                var sut = new Uri("http://localhost/path?name=value");

                var result = sut.SetQuery(names);

                Assert.That(result.AbsoluteUri, Is.EqualTo("http://localhost/path?name1&name2"));
            }
        }

        [TestFixture]
        public class SetFragment
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => UriSetExtensions.SetFragment(null, "fragment"));
            }

            [TestCase(null)]
            [TestCase("")]
            public void WhenFragmentIsNullOrEmpty_ThenRemoveFragment(string fragment)
            {
                var sut = new Uri("http://localhost/path?name=value#someFragment");

                var result = sut.SetFragment(fragment);

                Assert.That(result, Is.EqualTo(new Uri("http://localhost/path?name=value")));
            }

            [TestCase("newfrag")]
            [TestCase("#newfrag")]
            public void WhenHasNoFragment_ThenAddFragment(string fragment)
            {
                var sut = new Uri("http://localhost/path?name=value");

                var result = sut.SetFragment(fragment);

                Assert.That(result, Is.EqualTo(new Uri("http://localhost/path?name=value#newfrag")));
            }

            [TestCase("newfrag")]
            [TestCase("#newfrag")]
            public void WhenHasFragment_ThenUpdateFragment(string fragment)
            {
                var sut = new Uri("http://localhost/path?name=value#fragment");

                var result = sut.SetFragment(fragment);

                Assert.That(result, Is.EqualTo(new Uri("http://localhost/path?name=value#newfrag")));
            }
        }
    }
}