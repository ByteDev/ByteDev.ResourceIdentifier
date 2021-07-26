using System;
using System.Linq;
using NUnit.Framework;

namespace ByteDev.ResourceIdentifier.UnitTests
{
    [TestFixture]
    public class UriRemoveExtensionsTests
    {
        [TestFixture]
        public class RemovePath : UriRemoveExtensionsTests
        {
            [Test]
            public void WhenSourceIsNull_ThenReturnNull()
            {
                var result = UriRemoveExtensions.RemovePath(null);

                Assert.That(result, Is.Null);
            }

            [TestCase("http://localhost")]
            [TestCase("http://localhost/")]
            [TestCase("http://localhost/?")]
            [TestCase("http://localhost/?name=John")]
            [TestCase("http://localhost/?name=John#myfrag")]
            public void WhenUriHasNoPath_ThenReturnEqualUri(string uri)
            {
                var sut = new Uri(uri);

                var result = sut.RemovePath();

                Assert.That(result, Is.EqualTo(new Uri(uri)));
            }

            [TestCase("http://localhost/path1?name=John#myfrag")]
            [TestCase("http://localhost/path1/?name=John#myfrag")]
            [TestCase("http://localhost/path1/path2?name=John#myfrag")]
            [TestCase("http://localhost/path1/path2/?name=John#myfrag")]
            public void WhenUriHasPath_ThenRemovePath(string uri)
            {
                var sut = new Uri(uri);

                var result = sut.RemovePath();

                Assert.That(result, Is.EqualTo(new Uri("http://localhost/?name=John#myfrag")));
            }
        }

        [TestFixture]
        public class RemoveQuery
        {
            [Test]
            public void WhenSourceIsNull_ThenReturnNull()
            {
                var result = UriRemoveExtensions.RemoveQuery(null);

                Assert.That(result, Is.Null);
            }

            [Test]
            public void WhenUriHasNoQuery_ThenReturnSame()
            {
                var sut = new Uri("http://localhost/myapp");

                var result = sut.RemoveQuery();

                Assert.That(result, Is.EqualTo(sut));
            }
            
            [TestCase("http://localhost/myapp?", "http://localhost/myapp")]
            [TestCase("http://localhost/myapp?name", "http://localhost/myapp")]
            [TestCase("http://localhost/myapp?name=value", "http://localhost/myapp")]
            [TestCase("http://localhost/myapp/?name", "http://localhost/myapp/")]
            [TestCase("http://localhost/myapp/?name=value", "http://localhost/myapp/")]
            public void WhenUriHasQuery_ThenRemoveQuery(string uri, string expected)
            {
                var sut = new Uri(uri);

                var result = sut.RemoveQuery();

                Assert.That(result, Is.EqualTo(new Uri(expected)));
            }
        }

        [TestFixture]
        public class RemoveQueryParam
        {
            [Test]
            public void WhenSourceIsNull_ThenReturnNull()
            {
                var result = UriRemoveExtensions.RemoveQueryParam(null, "name");

                Assert.That(result, Is.Null);
            }
            
            [TestCase(null)]
            [TestCase("")]
            public void WhenNameIsNullOrEmpty_ThenReturnSame(string name)
            {
                var sut = new Uri("http://localhost/myapp");

                var result = sut.RemoveQueryParam(name);

                Assert.That(result, Is.SameAs(sut));
            }

            [Test]
            public void WhenNamesNotExist_ThenReturnEqual()
            {
                var sut = new Uri("http://localhost/myapp?name=value");

                var result = sut.RemoveQueryParam("nameNotExist");

                Assert.That(result, Is.EqualTo(sut));
            }

            [TestCase("http://localhost/myapp?name")]
            [TestCase("http://localhost/myapp?name=")]
            [TestCase("http://localhost/myapp?name=value")]
            [TestCase("http://localhost/myapp?NAME=value")]
            public void WhenNameExists_ThenRemoveParam(string uri)
            {
                var sut = new Uri(uri);

                var result = sut.RemoveQueryParam("name");

                Assert.That(result, Is.EqualTo(new Uri("http://localhost/myapp")));
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
            public void WhenSourceIsNull_ThenReturnNull()
            {
                var result = UriRemoveExtensions.RemoveQueryParams(null, Enumerable.Empty<string>());

                Assert.That(result, Is.Null);
            }
            
            [Test]
            public void WhenNamesIsNull_ThenReturnEqual()
            {
                var sut = new Uri("http://localhost/myapp");

                var result = sut.RemoveQueryParams(null);

                Assert.That(result, Is.EqualTo(sut));
            }

            [Test]
            public void WhenNamesIsEmpty_ThenReturnEqual()
            {
                var sut = new Uri("http://localhost/myapp?name=John");

                var result = sut.RemoveQueryParams(Enumerable.Empty<string>());

                Assert.That(result, Is.EqualTo(sut));
            }

            [Test]
            public void WhenNamesNotExist_ThenReturnEqual()
            {
                var sut = new Uri("http://localhost/myapp?name=value");

                var result = sut.RemoveQueryParams(new[] { "anothername" });

                Assert.That(result, Is.EqualTo(sut));
            }

            [TestCase("http://localhost/myapp?", "http://localhost/myapp")]
            [TestCase("http://localhost/myapp?name", "http://localhost/myapp")]
            [TestCase("http://localhost/myapp?name=", "http://localhost/myapp")]
            [TestCase("http://localhost/myapp?name=value", "http://localhost/myapp")]
            [TestCase("http://localhost/myapp?NAME=value", "http://localhost/myapp")]
            [TestCase("http://localhost/myapp/?name=value", "http://localhost/myapp/")]
            public void WhenNameExists_ThenRemoveParam(string uri, string expected)
            {
                var sut = new Uri(uri);

                var result = sut.RemoveQueryParams(new[] { "name" });

                Assert.That(result, Is.EqualTo(new Uri(expected)));
            }

            [Test]
            public void WhenBothNamesExist_ThenRemoveBothParam()
            {
                var sut = new Uri("http://localhost/myapp?name=John&surname=Smith&age=50");

                var result = sut.RemoveQueryParams(new[] { "name", "surname" });

                Assert.That(result, Is.EqualTo(new Uri("http://localhost/myapp?age=50")));
            }

            [Test]
            public void WhenCollectionHasNullOrEmptyName_ThenSkipNullOrEmptyName()
            {
                var sut = new Uri("http://localhost/myapp?name=John&surname=Smith&age=50");

                var result = sut.RemoveQueryParams(new[] { "name", null, string.Empty, "surname" });

                Assert.That(result, Is.EqualTo(new Uri("http://localhost/myapp?age=50")));
            }
        }

        [TestFixture]
        public class RemoveFragment
        {
            [Test]
            public void WhenSourceIsNull_ThenReturnNull()
            {
                var result = UriRemoveExtensions.RemoveFragment(null);

                Assert.That(result, Is.Null);
            }

            [Test]
            public void WhenHasNoFragment_ThenReturnEqual()
            {
                var sut = new Uri("http://localhost/myapp?name=John");

                var result = sut.RemoveFragment();

                Assert.That(result, Is.EqualTo(sut));
            }
            
            [TestCase("http://localhost/myapp?name=John#")]
            [TestCase("http://localhost/myapp?name=John#1")]
            [TestCase("http://localhost/myapp?name=John#somefragment")]
            public void WhenHasFragment_ThenReturnWithoutFragment(string uri)
            {
                var sut = new Uri(uri);

                var result = sut.RemoveFragment();

                Assert.That(result, Is.EqualTo(new Uri("http://localhost/myapp?name=John")));
            }
        }
    }
}