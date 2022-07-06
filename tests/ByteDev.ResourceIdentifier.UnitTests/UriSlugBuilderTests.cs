using System;
using NUnit.Framework;

namespace ByteDev.ResourceIdentifier.UnitTests
{
    [TestFixture]
    public class UriSlugBuilderTests
    {
        private UriSlugBuilder _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new UriSlugBuilder();
        }

        [Test]
        public void WhenTextNotSet_ThenReturnEmpty()
        {
            var result = _sut.Build();

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void WhenTextSet_ThenReplaceSpacesAndToLower()
        {
            var result = _sut
                .WithText("My First Blog Post")
                .Build();

            Assert.That(result, Is.EqualTo("my-first-blog-post"));
        }

        [Test]
        public void WhenTextHasRepeatingSpaces_ThenRemoveRepeatingSpace()
        {
            var result = _sut
                .WithText("My  First   Blog    Post")
                .Build();

            Assert.That(result, Is.EqualTo("my-first-blog-post"));
        }

        [Test]
        public void WhenTextHasStartAndEndSpaces_ThenTrim()
        {
            var result = _sut
                .WithText("  My First Blog Post ")
                .Build();

            Assert.That(result, Is.EqualTo("my-first-blog-post"));
        }

        [Test]
        public void WhenSpaceCharSet_ThenReplaceSpace()
        {
            var result = _sut
                .WithText("My First Blog Post")
                .WithSpaceChar('=')
                .Build();

            Assert.That(result, Is.EqualTo("my=first=blog=post"));
        }

        [Test]
        public void WhenSuffixDateTime_ThenAppend()
        {
            var result = _sut
                .WithText("My First Blog Post")
                .WithDateTimeSuffix(DateTime.UtcNow)
                .Build();

            StringAssert.StartsWith("my-first-blog-post-", result);
            Assert.That(result.Length, Is.GreaterThan("my-first-blog-post-".Length));

            Console.WriteLine(result);
        }

        [Test]
        public void WhenSuffixRandom_ThenAppend()
        {
            const int length = 5;

            var result = _sut
                .WithText("My First Blog Post")
                .WithRandomSuffix(length)
                .Build();

            StringAssert.StartsWith("my-first-blog-post-", result);
            Assert.That(result.Length, Is.GreaterThan("my-first-blog-post-".Length + length));

            Console.WriteLine(result);
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void WhenSuffixRandomLenLessThanOne_ThenAppendNothing(int length)
        {
            var result = _sut
                .WithText("My First Blog Post")
                .WithRandomSuffix(length)
                .Build();

            Assert.That(result, Is.EqualTo("my-first-blog-post"));
        }
    }
}