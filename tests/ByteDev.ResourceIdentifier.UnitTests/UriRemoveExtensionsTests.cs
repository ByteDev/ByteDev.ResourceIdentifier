using System;
using System.Linq;
using NUnit.Framework;

namespace ByteDev.ResourceIdentifier.UnitTests
{
    [TestFixture]
    public class UriRemoveExtensionsTests
    {
        [TestFixture]
        public class RemoveFragment
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => UriRemoveExtensions.RemoveFragment(null));
            }

            [Test]
            public void WhenHasNoFragment_ThenReturnWithoutFragment()
            {
                var sut = new Uri("http://localhost/myapp?name=John");

                var result = sut.RemoveFragment();

                Assert.That(result, Is.EqualTo(new Uri("http://localhost/myapp?name=John")));
            }
            
            [TestCase("http://localhost/myapp?name=John#")]
            [TestCase("http://localhost/myapp?name=John#somefragment")]
            public void WhenHasFragment_ThenReturnWithoutFragment(string uri)
            {
                var sut = new Uri(uri);

                var result = sut.RemoveFragment();

                Assert.That(result, Is.EqualTo(new Uri("http://localhost/myapp?name=John")));
            }
        }

        [TestFixture]
        public class RemoveQuery
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => UriRemoveExtensions.RemoveQuery(null));
            }

            [TestCase("http://localhost/myapp")]
            [TestCase("http://localhost/myapp?")]
            public void WhenHasNoQueryString_ThenReturnWithoutQuery(string uri)
            {
                var expected = new Uri("http://localhost/myapp");

                var sut = new Uri(uri);

                var result = sut.RemoveQuery();

                Assert.That(result, Is.EqualTo(expected));
            }
            
            [TestCase("http://localhost/myapp?name")]
            [TestCase("http://localhost/myapp?name=value")]
            public void WhenQueryStringHasKeys_ThenReturnWithoutQuery(string uri)
            {
                var expected = new Uri("http://localhost/myapp");

                var sut = new Uri(uri);

                var result = sut.RemoveQuery();

                Assert.That(result, Is.EqualTo(expected));
            }
        }

        [TestFixture]
        public class RemoveQueryParam
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => UriRemoveExtensions.RemoveQueryParam(null, "name"));
            }
            
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenDoNothing(string name)
            {
                var sut = new Uri("http://localhost/myapp");

                var result = sut.RemoveQueryParam(name);

                Assert.That(result.AbsoluteUri, Is.EqualTo(sut.AbsoluteUri));
            }

            [Test]
            public void WhenNamesNotExist_ThenNotRemoveName()
            {
                var expected = new Uri("http://localhost/myapp?name=value");

                var sut = new Uri(expected.ToString());

                var result = sut.RemoveQueryParam("anothername");

                Assert.That(result, Is.EqualTo(expected));
            }

            [TestCase("http://localhost/myapp?name=", "http://localhost/myapp")]
            [TestCase("http://localhost/myapp?name=value", "http://localhost/myapp")]
            [TestCase("http://localhost/myapp?NAME=value", "http://localhost/myapp")]
            public void WhenNameExists_ThenRemoveParam(string uri, string expected)
            {
                var sut = new Uri(uri);

                var result = sut.RemoveQueryParam("name");

                Assert.That(result, Is.EqualTo(new Uri(expected)));
            }

            [Test]
            public void WhenNameExistsWithOthers_ThenRemoveParam()
            {
                var sut = new Uri("http://localhost/myapp?name=John&surname=Smith");

                var result = sut.RemoveQueryParam("name");

                Assert.That(result, Is.EqualTo(new Uri("http://localhost/myapp?surname=Smith")));
            }
        }

        [TestFixture]
        public class RemoveQueryParams
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => UriRemoveExtensions.RemoveQueryParams(null, Enumerable.Empty<string>()));
            }
            
            [Test]
            public void WhenNamesIsNull_ThenReturnEqualUri()
            {
                var sut = new Uri("http://localhost/myapp");

                var result = sut.RemoveQueryParams(null);

                Assert.That(result, Is.EqualTo(sut));
            }

            [Test]
            public void WhenNamesIsEmpty_ThenReturnEqualUri()
            {
                var sut = new Uri("http://localhost/myapp?name=John");

                var result = sut.RemoveQueryParams(Enumerable.Empty<string>());

                Assert.That(result, Is.EqualTo(sut));
            }

            [Test]
            public void WhenNamesNotExist_ThenNotRemoveName()
            {
                var sut = new Uri("http://localhost/myapp?name=value");

                var result = sut.RemoveQueryParams(new[] { "anothername" });

                Assert.That(result, Is.EqualTo(new Uri("http://localhost/myapp?name=value")));
            }

            [TestCase("http://localhost/myapp?name", "http://localhost/myapp")]
            [TestCase("http://localhost/myapp?name=", "http://localhost/myapp")]
            [TestCase("http://localhost/myapp?name=value", "http://localhost/myapp")]
            [TestCase("http://localhost/myapp?NAME=value", "http://localhost/myapp")]
            public void WhenNameExists_ThenRemoveParam(string uri, string expected)
            {
                var sut = new Uri(uri);

                var result = sut.RemoveQueryParams(new[] { "name" });

                Assert.That(result, Is.EqualTo(new Uri(expected)));
            }

            [Test]
            public void WhenBothNamesExist_ThenRemoveBothParam()
            {
                var sut = new Uri("http://localhost/myapp?name=John&surname=Smith");

                var result = sut.RemoveQueryParams(new[] { "name", "surname" });

                Assert.That(result, Is.EqualTo(new Uri("http://localhost/myapp")));
            }

            [Test]
            public void WhenCollectionHasNullOrEmptyName_ThenSkipNullOrEmptyName()
            {
                var sut = new Uri("http://localhost/myapp?name=John&surname=Smith");

                var result = sut.RemoveQueryParams(new[] { "name", null, string.Empty, "surname" });

                Assert.That(result, Is.EqualTo(new Uri("http://localhost/myapp")));
            }
        }
    }
}