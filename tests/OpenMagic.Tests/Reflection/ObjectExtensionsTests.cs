using System;
using FluentAssertions;
using OpenMagic.DataAnnotations;
using OpenMagic.Reflection;
using Xunit;

namespace OpenMagic.Tests.Reflection
{
    public class ObjectExtensionsTests
    {
        public class Method
        {
            [Fact]
            public void ShouldReturnMethodInfoForRequestedMethod()
            {
                // Given
                var obj = new Exception();

                // When
                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                var methodInfo = obj.Method(x => x.ToString());

                // Then
                methodInfo.Name.Should().Be("ToString");
            }

            [Fact]
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

        public class Property
        {
            [Fact]
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