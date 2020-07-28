using System;
using NUnit.Framework;

namespace ByteDev.ResourceIdentifier.UnitTests
{
    [TestFixture]
    public class UriHasExtensionsTests
    {
        [TestFixture]
        public class HasPath
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => UriHasExtensions.HasPath(null));
            }

            [TestCase("http://localhost")]
            [TestCase("http://localhost/")]
            public void WhenHasNoPath_ThenReturnFalse(string uri)
            {
                var sut = new Uri(uri);

                var result = sut.HasPath();

                Assert.That(result, Is.False);
            }

            [TestCase("http://localhost/path")]
            [TestCase("http://localhost/path1/path2")]
            [TestCase("http://localhost/path1?q=1")]
            [TestCase("http://localhost/path#fragment")]
            public void WhenHasPath_ThenReturnTrue(string uri)
            {
                var sut = new Uri(uri);

                var result = sut.HasPath();

                Assert.That(result, Is.True);
            }
        }

        [TestFixture]
        public class HasQuery
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => UriHasExtensions.HasQuery(null));
            }

            [TestCase("http://local/")]
            [TestCase("http://local/app")]
            [TestCase("http://local/app?")]
            public void WhenHasNoQuery_ThenReturnFalse(string uri)
            {
                var sut = new Uri(uri);

                var result = sut.HasQuery();

                Assert.That(result, Is.False);
            }

            [TestCase("http://local/app?name")]
            [TestCase("http://local/app?name=John")]
            public void WhenHasQuery_ThenReturnTrue(string uri)
            {
                var sut = new Uri(uri);

                var result = sut.HasQuery();

                Assert.That(result, Is.True);
            }
        }

        [TestFixture]
        public class HasFragment
        {
            [Test]
            public void WhenSourceIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => UriHasExtensions.HasFragment(null));
            }

            [TestCase("http://local/app")]
            [TestCase("http://local/app#")]
            public void WhenHasNoFragment_ThenReturnFalse(string uri)
            {
                var sut = new Uri(uri);

                var result = sut.HasFragment();

                Assert.That(result, Is.False);
            }

            [TestCase("http://local/#fragment")]
            [TestCase("http://local/app#fragment")]
            [TestCase("http://local/app/#fragment")]
            [TestCase("http://local/app?query=1#fragment")]
            public void WhenHasFragment_ThenReturnTrue(string uri)
            {
                var sut = new Uri(uri);

                var result = sut.HasFragment();

                Assert.That(result, Is.True);
            }
        }
    }
}