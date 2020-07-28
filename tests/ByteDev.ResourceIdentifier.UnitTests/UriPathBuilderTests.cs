using NUnit.Framework;

namespace ByteDev.ResourceIdentifier.UnitTests
{
    [TestFixture]
    public class UriPathBuilderTests
    {
        [TestFixture]
        public class Build : UriPathBuilderTests
        {
            private UriPathBuilder _sut;

            [SetUp]
            public void SetUp()
            {
                _sut = new UriPathBuilder();
            }

            [Test]
            public void WhenPathIsNotAdded_ThenReturnSlashPath()
            {
                var result = _sut.Build();

                Assert.That(result, Is.EqualTo("/"));
            }

            [Test]
            public void WhenPathIsSlashOnly_ThenReturnSlashPath()
            {
                var result = _sut.AddPath("/").Build();

                Assert.That(result, Is.EqualTo("/"));
            }

            [Test]
            public void WhenPathHasSlashPrefix_ThenReturnPathWithSlash()
            {
                var result = _sut.AddPath("/mypath/").Build();

                Assert.That(result, Is.EqualTo("/mypath/"));
            }

            [Test]
            public void WhenPathHasNoSlashSuffix_ThenReturnPathWithNoSlashSuffix()
            {
                var result = _sut.AddPath("/mypath").Build();

                Assert.That(result, Is.EqualTo("/mypath"));
            }

            [Test]
            public void WhenPathHasNoSlashPrefix_ThenReturnPathWithSlash()
            {
                var result = _sut.AddPath("mypath/").Build();

                Assert.That(result, Is.EqualTo("/mypath/"));
            }

            [Test]
            public void WhenAddTwoPaths_ThenReturnPathsCombined()
            {
                var result = _sut
                    .AddPath("/mypath/")
                    .AddPath("/myotherpath/")
                    .Build();

                Assert.That(result, Is.EqualTo("/mypath/myotherpath/"));
            }

            [Test]
            public void WhenOneQueryParam_ThenReturnsPathAndQueryString()
            {
                var result = _sut
                    .AddPath("/mypath")
                    .AddOrModifyQueryStringParam("myname", "myvalue")
                    .Build();

                Assert.That(result, Is.EqualTo("/mypath?myname=myvalue"));
            }

            [Test]
            public void WhenTwoQueryParams_ThenReturnsQuerySeparatedByAmp()
            {
                var result = _sut
                    .AddPath("/mypath/")
                    .AddOrModifyQueryStringParam("myname1", "myvalue1")
                    .AddOrModifyQueryStringParam("myname2", "myvalue2")
                    .Build();

                Assert.That(result, Is.EqualTo("/mypath/?myname1=myvalue1&myname2=myvalue2"));
            }

            [Test]
            public void WhenSameParamAddedTwice_ThenModifyWithLastValue()
            {
                var result = _sut
                    .AddPath("/mypath/")
                    .AddOrModifyQueryStringParam("myname", "myvalue1")
                    .AddOrModifyQueryStringParam("myname", "myvalue2")
                    .Build();

                Assert.That(result, Is.EqualTo("/mypath/?myname=myvalue2"));                
            }

            [Test]
            public void WhenQueryParamAdded_AndNoPath_ThenReturnsWithForwardSlashAndQueryString()
            {
                var result = _sut
                    .AddOrModifyQueryStringParam("myname", "myvalue1")
                    .AddOrModifyQueryStringParam("myname", "myvalue2")
                    .Build();

                Assert.That(result, Is.EqualTo("/?myname=myvalue2"));                
            }
        }
    }
}