using System;
using System.Linq;
using ByteDev.Collections;
using NUnit.Framework;
using UriQueryToExtensions = ByteDev.ResourceIdentifier.UriQueryToExtensions;

namespace ByteDev.ResourceIdentifier.UnitTests
{
    [TestFixture]
    public class UriQueryToExtensionsTests
    {
        [TestFixture]
        public class QueryToDictionary
        {
            [Test]
            public void WhenSourceIsNull_ThenReturnEmpty()
            {
                var result = UriQueryToExtensions.QueryToDictionary(null);

                Assert.That(result, Is.Empty);
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
            public void WhenSourceIsNull_ThenReturnEmpty()
            {
                var result = UriQueryToExtensions.QueryToNameValueCollection(null);

                Assert.That(result, Is.Empty);
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
    }
}