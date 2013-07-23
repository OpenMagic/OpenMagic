using OpenMagic.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel.DataAnnotations;

namespace OpenMagic.Tests.Reflection
{
    public class PropertyInfoExtensionsTests
    {
        [TestClass]
        public class IsDecoratedWith
        {
            [TestMethod]
            public void Should_BeTrue_When_PropertyIsDecoratedWithNomitatedAttribute()
            {
                // Given
                var propertyInfo = Type<TestClass>.Property(x => x.HasRequiredAttribute);

                // When
                var result = propertyInfo.IsDecoratedWith<RequiredAttribute>();

                // Then
                result.Should().BeTrue();
            }

            [TestMethod]
            public void Should_BeFalse_When_PropertyIsNotDecoratedWithNomitedAttribute()
            {
                // Given
                var propertyInfo = Type<TestClass>.Property(x => x.HasNoAttributes);

                // When
                var result = propertyInfo.IsDecoratedWith<RequiredAttribute>();

                // Then
                result.Should().BeFalse();
            }

            [TestMethod]
            public void ShouldThrowArgumentNullExceptionWhenValueIsNull()
            {
                // When
                Action action = () => PropertyInfoExtensions.IsDecoratedWith<RequiredAttribute>(null);

                // Then
                action.ShouldThrow<ArgumentNullException>()
                    .Subject.Message.EndsWith("Parameter name: value");                    
            }
        }

        [TestClass]
        public class IsRequired
        {
            [TestMethod]
            public void Should_BeTrue_When_PropertyIsDecoratedWith_RequiredAttribute()
            {
                // Given
                var propertyInfo = Type<TestClass>.Property(x => x.HasRequiredAttribute);

                // When
                var result = propertyInfo.IsRequired();

                // Then
                result.Should().BeTrue();
            }

            [TestMethod]
            public void Should_BeFalse_When_PropertyIsNotDecoratedWith_RequiredAttribute()
            {
                // Given
                var propertyInfo = Type<TestClass>.Property(x => x.HasNoAttributes);

                // When
                var result = propertyInfo.IsRequired();

                // Then
                result.Should().BeFalse();
            }

            [TestMethod]
            public void ShouldThrowArgumentNullExceptionWhenValueIsNull()
            {
                // When
                Action action = () => PropertyInfoExtensions.IsRequired(null);

                // Then
                action.ShouldThrow<ArgumentNullException>()
                    .Subject.Message.EndsWith("Parameter name: value");
            }
        }

        public class TestClass
        {
            public int HasNoAttributes { get; set; }

            [Required]
            public int HasRequiredAttribute { get; set; }
        }
    }
}
