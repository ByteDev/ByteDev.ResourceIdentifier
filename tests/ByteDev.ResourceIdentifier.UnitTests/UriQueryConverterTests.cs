using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using ByteDev.Collections;
using NUnit.Framework;

namespace ByteDev.ResourceIdentifier.UnitTests
{
    [TestFixture]
    public class UriQueryConverterTests
    {
        [TestFixture]
        public class ToStringNameValueCollection
        {
            [Test]
            public void WhenIsNull_ThenReturnEmpty()
            {
                var result = UriQueryConverter.ToString(null as NameValueCollection);

                Assert.That(result, Is.Empty);
            }

            [Test]
            public void WhenIsEmpty_ThenReturnEmpty()
            {
                var result = UriQueryConverter.ToString(new NameValueCollection());

                Assert.That(result, Is.Empty);
            }

            [Test]
            public void WhenHasOnePair_ThenReturnAsString()
            {
                var nameValues = new NameValueCollection
                {
                    {"key", "value"}
                };

                var result = UriQueryConverter.ToString(nameValues);

                Assert.That(result, Is.EqualTo("?key=value"));
            }

            [Test]
            public void WhenHasTwoPairs_ThenReturnWithAmpDelimiter()
            {
                var nameValues = new NameValueCollection
                {
                    {"key1", "value1"},
                    {"key2", "value2"}
                };

                var result = UriQueryConverter.ToString(nameValues);

                Assert.That(result, Is.EqualTo("?key1=value1&key2=value2"));
            }
        }

        [TestFixture]
        public class ToStringEnumerable : UriQueryConverterTests
        {
            [Test]
            public void WhenIsNull_ThenReturnEmpty()
            {
                var result = UriQueryConverter.ToString(null as IEnumerable<string>);

                Assert.That(result, Is.Empty);
            }

            [Test]
            public void WhenIsEmpty_ThenReturnEmpty()
            {
                var result = UriQueryConverter.ToString(Enumerable.Empty<string>());

                Assert.That(result, Is.Empty);
            }

            [Test]
            public void WhenHasOneName_ThenReturnAsString()
            {
                var names = new List<string> { "name1" };

                var result = UriQueryConverter.ToString(names);

                Assert.That(result, Is.EqualTo("?name1"));
            }

            [Test]
            public void WhenHasTwoNames_ThenReturnAsString()
            {
                var names = new List<string> { "name1", "name2" };

                var result = UriQueryConverter.ToString(names);

                Assert.That(result, Is.EqualTo("?name1&name2"));
            }

            [Test]
            public void WhenHasMultipleSameNames_ThenReturnWithoutDuplicates()
            {
                var names = new List<string> { "name1", "name2", "name1", "name3" };

                var result = UriQueryConverter.ToString(names);

                Assert.That(result, Is.EqualTo("?name1&name2&name3"));
            }
        }

        [TestFixture]
        public class ToStringDictionary
        {
            [Test]
            public void WhenIsNull_ThenReturnEmpty()
            {
                var result = UriQueryConverter.ToString(null as IDictionary<string, string>);

                Assert.That(result, Is.Empty);
            }

            [Test]
            public void WhenIsEmpty_ThenReturnEmpty()
            {
                var result = UriQueryConverter.ToString(new Dictionary<string, string>());

                Assert.That(result, Is.Empty);
            }

            [Test]
            public void WhenHasOnePair_ThenReturnAsString()
            {
                var nameValues = new Dictionary<string, string>
                {
                    {"key", "value"}
                };

                var result = UriQueryConverter.ToString(nameValues);

                Assert.That(result, Is.EqualTo("?key=value"));
            }

            [Test]
            public void WhenHasTwoPairs_ThenReturnWithAmpDelimiter()
            {
                var nameValues = new Dictionary<string, string>
                {
                    {"key1", "value1"},
                    {"key2", "value2"}
                };

                var result = UriQueryConverter.ToString(nameValues);

                Assert.That(result, Is.EqualTo("?key1=value1&key2=value2"));
            }
        }

        [TestFixture]
        public class ToNameValueCollection
        {
            [TestCase(null)]
            [TestCase("")]
            [TestCase("?")]
            public void WhenIsNullOrEmptyOrQuestionMark_ThenReturnEmpty(string query)
            {
                var result = UriQueryConverter.ToNameValueCollection(query);

                Assert.That(result, Is.Empty);
            }

            [Test]
            public void WhenValidQueryWithQuestionMark_ThenReturnNameValues()
            {
                var result = UriQueryConverter.ToNameValueCollection("?key1=value1&key2=value2");

                Assert.That(result.AllKeys.Length, Is.EqualTo(2));
                Assert.That(result.GetValues("key1").Single(), Is.EqualTo("value1"));
                Assert.That(result.GetValues("key2").Single(), Is.EqualTo("value2"));
            }

            [Test]
            public void WhenValidQueryWithoutQuestionMark_ThenReturnNameValues()
            {
                var result = UriQueryConverter.ToNameValueCollection("key1=value1&key2=value2");

                Assert.That(result.AllKeys.Length, Is.EqualTo(2));
                Assert.That(result.GetValues("key1").Single(), Is.EqualTo("value1"));
                Assert.That(result.GetValues("key2").Single(), Is.EqualTo("value2"));
            }

            [Test]
            public void WhenQueryNoValue_ThenReturnNameValueCollection()
            {
                var result = UriQueryConverter.ToNameValueCollection("?key1&key2&key3=value3");

                Assert.That(result.AllKeys.Length, Is.EqualTo(3));
                Assert.That(result.GetValues("key1"), Is.Null);
                Assert.That(result.GetValues("key2"), Is.Null);
                Assert.That(result.GetValues("key3").Single(), Is.EqualTo("value3"));
            }
        }

        [TestFixture]
        public class ToDictionary
        {
            [TestCase(null)]
            [TestCase("")]
            [TestCase("?")]
            public void WhenIsNullOrEmptyOrQuestionMark_ThenReturnEmpty(string query)
            {
                var result = UriQueryConverter.ToDictionary(query);

                Assert.That(result, Is.Empty);
            }
            
            [Test]
            public void WhenValidQueryWithQuestionMark_ThenReturnNameValues()
            {
                var result = UriQueryConverter.ToDictionary("?key1=value1&key2=value2");

                Assert.That(result.Count, Is.EqualTo(2));
                Assert.That(result["key1"], Is.EqualTo("value1"));
                Assert.That(result["key2"], Is.EqualTo("value2"));
            }

            [Test]
            public void WhenValidQueryWithoutQuestionMark_ThenReturnNameValues()
            {
                var result = UriQueryConverter.ToDictionary("key1=value1&key2=value2");

                Assert.That(result.Count, Is.EqualTo(2));
                Assert.That(result["key1"], Is.EqualTo("value1"));
                Assert.That(result["key2"], Is.EqualTo("value2"));
            }

            [Test]
            public void WhenQueryNoValue_ThenReturnNameValueCollection()
            {
                var result = UriQueryConverter.ToDictionary("?key1&key2&key3=value3");

                Assert.That(result.Count, Is.EqualTo(3));
                Assert.That(result["key1"], Is.Null);
                Assert.That(result["key2"], Is.Null);
                Assert.That(result["key3"], Is.EqualTo("value3"));
            }
        }
    }
}