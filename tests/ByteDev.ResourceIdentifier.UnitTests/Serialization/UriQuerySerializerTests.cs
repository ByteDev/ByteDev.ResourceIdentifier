using System;
using NUnit.Framework;

namespace ByteDev.ResourceIdentifier.UnitTests.Serialization
{
    [TestFixture]
    public class UriQuerySerializerTests
    {
        // [TestFixture]
        // public class AddOrUpdateQueryParamsObject
        // {
        //     private ObjNoProperties _objNoProperties;
        //     private ObjOneProperty _objOneProperty;
        //     private ObjTwoProperties _objTwoProperties;
        //
        //     [SetUp]
        //     public void SetUp()
        //     {
        //         _objNoProperties = new ObjNoProperties();
        //         _objOneProperty = new ObjOneProperty { Name = "Peter" };
        //         _objTwoProperties = new ObjTwoProperties {Name = "John", Age = 20};
        //     }
        //
        //     [Test]
        //     public void WhenSourceIsNull_ThenThrowException()
        //     {
        //         Assert.Throws<ArgumentNullException>(() => UriExtensions.AddOrUpdateQueryParams(null, _objTwoProperties));
        //     }
        //     
        //     [Test]
        //     public void WhenObjectIsNull_ThenThrowException()
        //     {
        //         var sut = new Uri("http://localhost/myapp");
        //
        //         Assert.Throws<ArgumentNullException>(() => sut.AddOrUpdateQueryParams(null));
        //     }
        //
        //     [Test]
        //     public void WhenObjectHasNoProperties_ThenDontAppendToQueryString()
        //     {
        //         var sut = new Uri("http://localhost/myapp");
        //
        //         var result = sut.AddOrUpdateQueryParams(_objNoProperties);
        //
        //         Assert.That(result, Is.EqualTo(sut));
        //     }
        //
        //     [Test]
        //     public void WhenObjectOnePropertySet_ThenReturnWithQueryString()
        //     {
        //         var sut = new Uri("http://localhost/myapp");
        //
        //         var result = sut.AddOrUpdateQueryParams(_objOneProperty);
        //
        //         Assert.That(result, Is.EqualTo(new Uri($"http://localhost/myapp?Name={_objOneProperty.Name}")));
        //     }
        //
        //     [Test]
        //     public void WhenObjectOnePropertySetToEmpty_ThenReturnWithQueryString()
        //     {
        //         _objOneProperty.Name = string.Empty;
        //
        //         var sut = new Uri("http://localhost/myapp");
        //
        //         var result = sut.AddOrUpdateQueryParams(_objOneProperty);
        //
        //         Assert.That(result, Is.EqualTo(new Uri($"http://localhost/myapp?Name=")));
        //     }
        //
        //     [Test]
        //     public void WhenObjectTwoPropertiesSet_ThenReturnWithQueryString()
        //     {
        //         var sut = new Uri("http://localhost/myapp");
        //
        //         var result = sut.AddOrUpdateQueryParams(_objTwoProperties);
        //
        //         Assert.That(result, Is.EqualTo(new Uri($"http://localhost/myapp?Name={_objTwoProperties.Name}&Age={_objTwoProperties.Age}")));
        //     }
        //
        //     [Test]
        //     public void WhenUriAlreadyHasProperties_ThenReplace()
        //     {
        //         var sut = new Uri("http://localhost/myapp?Name=Dave&Age=50&Children=2");
        //
        //         var result = sut.AddOrUpdateQueryParams(_objTwoProperties);
        //
        //         Assert.That(result, Is.EqualTo(new Uri($"http://localhost/myapp?Name={_objTwoProperties.Name}&Age={_objTwoProperties.Age}&Children=2")));
        //     }
        //
        //     [Test]
        //     public void WhenPropertyIsNull_ThenDoNotAddToQueryString()
        //     {
        //         _objTwoProperties.Name = null;
        //
        //         var sut = new Uri("http://localhost/myapp");
        //
        //         var result = sut.AddOrUpdateQueryParams(_objTwoProperties);
        //
        //         Assert.That(result, Is.EqualTo(new Uri($"http://localhost/myapp?Age={_objTwoProperties.Age}")));
        //     }
        //
        //     public class ObjNoProperties
        //     {
        //     }
        //
        //     public class ObjOneProperty
        //     {
        //         public string Name { get; set; }
        //     }
        //
        //     public class ObjTwoProperties
        //     {
        //         public string Name { get; set; }
        //         public int Age { get; set; }
        //     }
        // }
    }
}