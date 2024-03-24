using System;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using OpenMagic.DataAnnotations;
using Xunit;

namespace OpenMagic.Tests.DataAnnotations;

public class ValidationTests
{
    public class TestClass
    {
        [Required] public string Required { get; set; }
    }

    public class Validate
    {
        [Fact]
        public void ShouldThrowValidationExceptionWhenValueIsNotValid()
        {
            // Given
            var invalidObject = new TestClass();

            // When
            Action action = () => invalidObject.Validate();

            // Then
            action.Should().Throw<ValidationException>();
        }

        [Fact]
        public void ShouldBeSameAsValueWhenValueIsValid()
        {
            // Given
            var validObject = new TestClass { Required = "required property" };

            // When
            var result = validObject.Validate();

            // Then
            result.Should().BeSameAs(validObject);
        }
    }
}