using System;
using NUnit.Framework;

namespace ByteDev.ResourceIdentifier.UnitTests
{
    [TestFixture]
    public class UriSetExtensionsTests
    {
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
            public void WhenHasNoQuestionMark_ThenSetQuery()
            {
                var sut = new Uri("http://localhost/path?name=value");

                var result = sut.SetQuery("age=50");

                Assert.That(result, Is.EqualTo(new Uri("http://localhost/path?age=50")));
            }

            [Test]
            public void WhenHasQuestionMark_ThenSetQuery()
            {
                var sut = new Uri("http://localhost/path?name=value");

                var result = sut.SetQuery("?age=50");

                Assert.That(result, Is.EqualTo(new Uri("http://localhost/path?age=50")));
            }

            [Test]
            public void WhenHashSpaces_ThenSetQuery()
            {
                var sut = new Uri("http://localhost/path?name=value");

                var result = sut.SetQuery("name=John Smith");

                Assert.That(result.AbsoluteUri, Is.EqualTo("http://localhost/path?name=John%20Smith"));
            }

            [Test]
            public void WhenStringCollection_ThenSetQuery()
            {
                var names = new[] { "name1", "name2", "name3" };

                var sut = new Uri("http://localhost/path?name=value");

                var result = sut.SetQuery(names);

                Assert.That(result.AbsoluteUri, Is.EqualTo("http://localhost/path?name1&name2&name3"));
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
            public void WhenPathIsNullOrEmpty_ThenRemovePath(string path)
            {
                var sut = new Uri("http://localhost/path?name=value");

                var result = sut.SetPath(path);

                Assert.That(result, Is.EqualTo(new Uri("http://localhost/?name=value")));
            }

            [Test]
            public void WhenPathValid_ThenSetPath()
            {
                var sut = new Uri("http://localhost/path?name=value#fragment");

                var result = sut.SetPath("newpath");

                Assert.That(result, Is.EqualTo(new Uri("http://localhost/newpath?name=value#fragment")));
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
            public void WhenFragmentIsNullOrEmpty_ThenRemovePath(string path)
            {
                var sut = new Uri("http://localhost/path?name=value#someFragment");

                var result = sut.SetFragment(path);

                Assert.That(result, Is.EqualTo(new Uri("http://localhost/path?name=value")));
            }

            [Test]
            public void WhenPathValid_ThenSetPath()
            {
                var sut = new Uri("http://localhost/path?name=value#fragment");

                var result = sut.SetFragment("newfrag");

                Assert.That(result, Is.EqualTo(new Uri("http://localhost/path?name=value#newfrag")));
            }
        }
    }
}