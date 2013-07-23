using OpenMagic.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace OpenMagic.Tests.Reflection
{
    public class ObjectExtensionsTests
    {
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

            [TestMethod]
            public void ShouldThrowArgumentNullExceptionWhenObjIsNull()
            {
                // Given
                Exception obj = null;

                // When
                Action action = () => obj.Property<Exception, object>(null);

                // Then
                action.ShouldThrow<ArgumentNullException>()
                    .Subject.Message.EndsWith("Parameter name: obj");
            }

            [TestMethod]
            public void ShouldThrowArgumentNullExceptionWhenPropertyIsNull()
            {
                // Given
                var obj = new Exception();

                // When
                Action action = () => obj.Property<Exception, object>(null);

                // Then
                action.ShouldThrow<ArgumentNullException>()
                    .Subject.Message.EndsWith("Parameter name: property");
            }
        }
    }
}
