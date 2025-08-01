﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using JetBrains.Annotations;
using OpenMagic.DataAnnotations;
using Xunit;

namespace OpenMagic.Tests.DataAnnotations
{
    public class PropertyMetadataTests
    {
        public class Constructor : PropertyMetadataTests
        {
            [Fact]
            public void ShouldThrowArgumentNullExceptionWhenPropertyInfoIsNull()
            {
                // ReSharper disable once ObjectCreationAsStatement
                Action action = () => new PropertyMetadata(null!, true);

                action.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("property");
            }
        }

        public class Display : PropertyMetadataTests
        {
            [Fact]
            public void ShouldReturnTheDisplayAttributeAssociatedToAProperty()
            {
                // Given
                var metadata = ClassMetadata.GetProperty<TestClassWithDisplayAttribute, int>(x => x.HasDisplayAttribute);

                // When
                var display = metadata.Display;

                // Then
                display.Should().NotBeNull();
            }

            [Fact]
            public void ShouldReturnADefaultDisplayAttributeForAPropertyThatDoesNotHaveADisplayAttribute()
            {
                // Given
                var metadata = ClassMetadata.GetProperty<TestClassWithDisplayAttribute, int>(x => x.DoesNotHaveDisplayAttribute);

                // When
                var display = metadata.Display;

                // Then
                display.Should().NotBeNull();
            }
        }

        public class IsNotPublic : PropertyMetadataTests
        {
            [Fact]
            public void ShouldReturnTrueWhenPropertyIsNotPublic()
            {
                // Given
                var privateProperty = typeof(Exception).GetProperties(BindingFlags.NonPublic | BindingFlags.Instance).First();
                var metadata = new PropertyMetadata(privateProperty, false);

                // When
                var isNotPublic = metadata.IsNotPublic;

                // Then
                isNotPublic.Should().BeTrue();
            }

            [Fact]
            public void ShouldReturnFalseWhenPropertyIsNotPublic()
            {
                // Given
                var publicProperty = typeof(Exception).GetProperties(BindingFlags.Public | BindingFlags.Instance).First();
                var metadata = new PropertyMetadata(publicProperty, true);

                // When
                var isNotPublic = metadata.IsNotPublic;

                // Then
                isNotPublic.Should().BeFalse();
            }
        }

        public class IsPublic : PropertyMetadataTests
        {
            [Fact]
            public void ShouldReturnTrueWhenPropertyIsPublic()
            {
                // Given
                var publicProperty = typeof(Exception).GetProperties(BindingFlags.Public | BindingFlags.Instance).First();
                var metadata = new PropertyMetadata(publicProperty, true);

                // When
                var isPublic = metadata.IsPublic;

                // Then
                isPublic.Should().BeTrue();
            }

            [Fact]
            public void ShouldReturnFalseWhenPropertyIsNotPublic()
            {
                // Given
                var privateProperty = typeof(Exception).GetProperties(BindingFlags.NonPublic | BindingFlags.Instance).First();
                var metadata = new PropertyMetadata(privateProperty, false);

                // When
                var isPublic = metadata.IsPublic;

                // Then
                isPublic.Should().BeFalse();
            }
        }

        [UsedImplicitly]
        private class TestClassWithDisplayAttribute
        {
            [Display] public int HasDisplayAttribute { get; [UsedImplicitly] set; }

            public int DoesNotHaveDisplayAttribute { get; [UsedImplicitly] set; }
        }
    }
}