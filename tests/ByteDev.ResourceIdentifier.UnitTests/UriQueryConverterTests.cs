using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using NUnit.Framework;

namespace ByteDev.ResourceIdentifier.UnitTests
{
    [TestFixture]
    public class UriQueryConverterTests
    {
        [TestFixture]
        public class ToString_NameValueCollection
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
            public void WhenHasOnePair_ThenReturnString()
            {
                var nameValues = new NameValueCollection
                {
                    {"key", "value"}
                };

                var result = UriQueryConverter.ToString(nameValues);

                Assert.That(result, Is.EqualTo("?key=value"));
            }

            [Test]
            public void WhenHasTwoPairs_ThenReturnString()
            {
                var nameValues = new NameValueCollection
                {
                    {"key1", "value1"},
                    {"key2", "value2"}
                };

                var result = UriQueryConverter.ToString(nameValues);

                Assert.That(result, Is.EqualTo("?key1=value1&key2=value2"));
            }

            [Test]
            public void WhenParamNameAndValueContainsEncodableChars_ThenEncodeNamesAndValues()
            {
                var nameValues = new NameValueCollection
                {
                    {"key1", "value1"},
                    {"key 2", "value 2"},
                    {"key/3", "value/3"}
                };

                var result = UriQueryConverter.ToString(nameValues);

                Assert.That(result, Is.EqualTo("?key1=value1&key+2=value+2&key%2f3=value%2f3"));
            }
        }

        [TestFixture]
        public class ToString_Dictionary
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
            public void WhenHasOnePair_ThenReturnString()
            {
                var dict = new Dictionary<string, string>
                {
                    {"key1", "value1"}
                };
                
                var result = UriQueryConverter.ToString(dict);

                Assert.That(result, Is.EqualTo("?key1=value1"));
            }

            [Test]
            public void WhenHasTwoPairs_ThenReturnString()
            {
                var dict = new Dictionary<string, string>
                {
                    {"key1", "value1"},
                    {"key2", "value2"}
                };

                var result = UriQueryConverter.ToString(dict);

                Assert.That(result, Is.EqualTo("?key1=value1&key2=value2"));
            }

            [Test]
            public void WhenParamNameAndValueContainsEncodableChars_ThenReturnString()
            {
                var dict = new Dictionary<string, string>
                {
                    {"key1", "value1"},
                    {"key 2", "value 2"},
                    {"key/3", "value/3"}
                };

                var result = UriQueryConverter.ToString(dict);

                Assert.That(result, Is.EqualTo("?key1=value1&key+2=value+2&key%2f3=value%2f3"));
            }
        }

        [TestFixture]
        public class ToString_Enumerable : UriQueryConverterTests
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
            public void WhenHasOneName_ThenReturnString()
            {
                var names = new List<string> { "name1" };

                var result = UriQueryConverter.ToString(names);

                Assert.That(result, Is.EqualTo("?name1"));
            }

            [Test]
            public void WhenHasTwoNames_ThenReturnString()
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

            [Test]
            public void WhenParamNamesContainEncodableChar_ThenEncodeNames()
            {
                var names = new List<string> { "name1", "name 2", "name/3" };

                var result = UriQueryConverter.ToString(names);

                Assert.That(result, Is.EqualTo("?name1&name+2&name%2f3"));
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
            public void WhenValidQueryWithoutQuestionMark_ThenReturnNameValues()
            {
                var result = UriQueryConverter.ToNameValueCollection("key1=value1&key2=value2");

                Assert.That(result.AllKeys.Length, Is.EqualTo(2));
                Assert.That(result["key1"], Is.EqualTo("value1"));
                Assert.That(result["key2"], Is.EqualTo("value2"));
            }

            [Test]
            public void WhenValidQueryWithQuestionMark_ThenReturnNameValues()
            {
                var result = UriQueryConverter.ToNameValueCollection("?key1=value1&key2=value2");

                Assert.That(result.AllKeys.Length, Is.EqualTo(2));
                Assert.That(result["key1"], Is.EqualTo("value1"));
                Assert.That(result["key2"], Is.EqualTo("value2"));
            }

            [Test]
            public void WhenQueryNoValue_ThenReturnNameValues()
            {
                var result = UriQueryConverter.ToNameValueCollection("?key1&key2&key3=value3");

                Assert.That(result.AllKeys.Length, Is.EqualTo(3));
                Assert.That(result["key1"], Is.Null);
                Assert.That(result["key2"], Is.Null);
                Assert.That(result["key3"], Is.EqualTo("value3"));
            }

            [Test]
            public void WhenParamNameAndValueContainsEncodableChars_ThenDecodeNamesAndValues()
            {
                var result = UriQueryConverter.ToNameValueCollection("?key1=value1&key+2=value+2&key%2f3=value%2f3");

                Assert.That(result["key1"], Is.EqualTo("value1"));
                Assert.That(result["key 2"], Is.EqualTo("value 2"));
                Assert.That(result["key/3"], Is.EqualTo("value/3"));
            }

            [Test]
            public void WhenValueContainsEquals_ThenReturnNameValues()
            {
                var result = UriQueryConverter.ToNameValueCollection("?key1=value=1&key2=value2==");

                Assert.That(result.AllKeys.Length, Is.EqualTo(2));
                Assert.That(result["key1"], Is.EqualTo("value=1"));
                Assert.That(result["key2"], Is.EqualTo("value2=="));
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
            public void WhenQueryNoValue_ThenReturnNameValues()
            {
                var result = UriQueryConverter.ToDictionary("?key1&key2&key3=value3");

                Assert.That(result.Count, Is.EqualTo(3));
                Assert.That(result["key1"], Is.Null);
                Assert.That(result["key2"], Is.Null);
                Assert.That(result["key3"], Is.EqualTo("value3"));
            }

            [Test]
            public void WhenParamNameAndValueContainsEncodableChars_ThenDecodeNamesAndValues()
            {
                var result = UriQueryConverter.ToDictionary("?key1=value1&key+2=value+2&key%2f3=value%2f3");

                Assert.That(result["key1"], Is.EqualTo("value1"));
                Assert.That(result["key 2"], Is.EqualTo("value 2"));
                Assert.That(result["key/3"], Is.EqualTo("value/3"));
            }

            [Test]
            public void WhenValueContainsEquals_ThenReturnNameValues()
            {
                var result = UriQueryConverter.ToDictionary("?key1=value=1&key2=value2==");

                Assert.That(result.Count, Is.EqualTo(2));
                Assert.That(result["key1"], Is.EqualTo("value=1"));
                Assert.That(result["key2"], Is.EqualTo("value2=="));
            }
        }
    }
}