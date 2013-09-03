using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenMagic.Reflection;

namespace OpenMagic.Tests.Reflection
{
    public class PropertyInfoExtensionsTests
    {
        [TestClass]
        public class GetCustomAttribute
        {
            [TestMethod]
            public void ShouldReturnNullWhenPropertyIsNotDecoratedWithAttribute()
            {
                // Given
                var propertyInfo = Type<TestClass>.Property(x => x.HasNoAttributes);

                // When
                var attribute = propertyInfo.GetCustomAttribute<RequiredAttribute>();

                // Then
                attribute.Should().BeNull();
            }

            [TestMethod]
            public void ShouldReturnAttributeWhenPropertyIsDecoratedWithAttribute()
            {
                // Given
                var propertyInfo = Type<TestClass>.Property(x => x.HasRequiredAttribute);

                // When
                var attribute = propertyInfo.GetCustomAttribute<RequiredAttribute>();

                // Then
                attribute.Should().NotBeNull().And.BeOfType<RequiredAttribute>();
            }
        }

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
        }

        public class TestClass
        {
            public int HasNoAttributes { get; set; }

            [Required]
            public int HasRequiredAttribute { get; set; }
        }
    }
}
