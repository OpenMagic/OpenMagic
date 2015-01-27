using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using OpenMagic.Reflection;
using Xunit;

namespace OpenMagic.Tests.Reflection
{
    public class PropertyInfoExtensionsTests
    {
        public class GetCustomAttribute
        {
            [Fact]
            public void ShouldReturnNullWhenPropertyIsNotDecoratedWithAttribute()
            {
                // Given
                var propertyInfo = Type<TestClass>.Property(x => x.HasNoAttributes);

                // When
                var attribute = propertyInfo.GetCustomAttribute<RequiredAttribute>();

                // Then
                attribute.Should().BeNull();
            }

            [Fact]
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

        public class IsDecoratedWith
        {
            [Fact]
            public void Should_BeTrue_When_PropertyIsDecoratedWithNomitatedAttribute()
            {
                // Given
                var propertyInfo = Type<TestClass>.Property(x => x.HasRequiredAttribute);

                // When
                var result = propertyInfo.IsDecoratedWith<RequiredAttribute>();

                // Then
                result.Should().BeTrue();
            }

            [Fact]
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

        public class IsRequired
        {
            [Fact]
            public void Should_BeTrue_When_PropertyIsDecoratedWith_RequiredAttribute()
            {
                // Given
                var propertyInfo = Type<TestClass>.Property(x => x.HasRequiredAttribute);

                // When
                var result = propertyInfo.IsRequired();

                // Then
                result.Should().BeTrue();
            }

            [Fact]
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