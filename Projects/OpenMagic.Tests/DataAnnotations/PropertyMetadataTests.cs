using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenMagic.DataAnnotations;
using TestMagic;

namespace OpenMagic.Tests.DataAnnotations
{
    public class PropertyMetadataTests
    {
        [TestClass]
        public class Constructor : PropertyMetadataTests
        {
            [TestMethod]
            public void ShouldThrowArgumentNullExceptionWhenPropertyInfoIsNull()
            {
                GWT.Given("testing constructor")
                    .When(x => new PropertyMetadata(property: null, isPublic: true))
                    .Then<ArgumentNullException>().ShouldBeThrown().ForParameter("property");
            }
        }

        [TestClass]
        public class Display : PropertyMetadataTests
        {
            [TestMethod]
            public void ShouldReturnTheDisplayAttributeAssociatedToAProperty()
            {
                // Given
                var metadata = ClassMetadata.GetProperty<TestClassWithDisplayAttribute, int>(x => x.HasDisplayAttribute);

                // When
                var display = metadata.Display;

                // Then
                display.Should().NotBeNull();
            }

            [TestMethod]
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

        [TestClass]
        public class IsNotPublic : PropertyMetadataTests
        {
            [TestMethod]
            public void ShouldReturnTrueWhenPropertyIsNotPublic()
            {
                // Given
                var privateProperty = typeof(Exception).GetProperties(BindingFlags.NonPublic | BindingFlags.Instance).First();
                var metadata = new PropertyMetadata(privateProperty, isPublic: false);

                // When
                var isNotPublic = metadata.IsNotPublic;

                // Then
                isNotPublic.Should().BeTrue();
            }

            [TestMethod]
            public void ShouldReturnFalseWhenPropertyIsNotPublic()
            {
                // Given
                var publicProperty = typeof(Exception).GetProperties(BindingFlags.Public | BindingFlags.Instance).First();
                var metadata = new PropertyMetadata(publicProperty, isPublic: true);

                // When
                var isNotPublic = metadata.IsNotPublic;

                // Then
                isNotPublic.Should().BeFalse();
            }
        }

        [TestClass]
        public class IsPublic : PropertyMetadataTests
        {
            [TestMethod]
            public void ShouldReturnTrueWhenPropertyIsPublic()
            {
                // Given
                var publicProperty = typeof(Exception).GetProperties(BindingFlags.Public | BindingFlags.Instance).First();
                var metadata = new PropertyMetadata(publicProperty, isPublic: true);

                // When
                var isPublic = metadata.IsPublic;

                // Then
                isPublic.Should().BeTrue();
            }

            [TestMethod]
            public void ShouldReturnFalseWhenPropertyIsNotPublic()
            {
                // Given
                var privateProperty = typeof(Exception).GetProperties(BindingFlags.NonPublic | BindingFlags.Instance).First();
                var metadata = new PropertyMetadata(privateProperty, isPublic: false);

                // When
                var isPublic = metadata.IsPublic;

                // Then
                isPublic.Should().BeFalse();
            }
        }

        private class TestClassWithDisplayAttribute
        {
            [Display]
            public int HasDisplayAttribute { get; set; }

            public int DoesNotHaveDisplayAttribute { get; set; }
        }
    }
}
