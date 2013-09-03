using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenMagic.DataAnnotations;
using OpenMagic.Reflection;

namespace OpenMagic.Tests.Extensions
{
    public class ObjectExtensionsTests
    {
        [TestClass]
        public class Method
        {
            [TestMethod]
            public void ShouldReturnMethodInfoForRequestedMethod()
            {
                // Given
                var obj = new Exception();

                // When
                var methodInfo = obj.Method(x => x.ToString());

                // Then
                methodInfo.Name.Should().Be("ToString");
            }

            [TestMethod]
            public void ShouldReturnMethodInfoForRequestedMethodThatTakesAnArgument()
            {
                // Given
                var obj = new UriAttribute();

                // When
                var methodInfo = obj.Method(x => x.IsValid(null));

                // Then
                methodInfo.Name.Should().Be("IsValid");
            }
        }

        [TestClass]
        public class Property
        {
            [TestMethod]
            public void Should_Be_PropertyInfo_ForRequestedProperty()
            {
                // Given
                var obj = new Exception();

                // When
                var propertyInfo = obj.Property(x => x.Message);

                // Then
                propertyInfo.Name.Should().Be("Message");
            }
        }
    }
}
