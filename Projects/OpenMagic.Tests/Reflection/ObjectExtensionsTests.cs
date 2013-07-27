using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenMagic.Reflection;
using OpenMagic.DataAnnotations;

namespace OpenMagic.Tests.Reflection
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

            [TestMethod]
            public void ShouldThrowArgumentNullExceptionWhen_obj_IsNull()
            {
                // Given

                // When
                Action action = () => ObjectExtensions.Method<object>(obj: null, method: null);

                // Then
                action.ShouldThrow<ArgumentNullException>().And.ParamName.Should().Be("obj");
            }

            [TestMethod]
            public void ShouldThrowArgumentNullExceptionWhen_method_IsNull()
            {
                // Given
                var obj = new Exception();

                // When
                Action action = () => obj.Method<Exception>(method: null);

                // Then
                action.ShouldThrow<ArgumentNullException>().And.ParamName.Should().Be("method");
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
