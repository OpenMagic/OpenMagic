using System;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenMagic.Collections.Generic;
using OpenMagic.DataAnnotations;
using TestMagic;

namespace OpenMagic.Tests.DataAnnotations
{
    public class ClassMetadataTests
    {
        public ClassMetadataTests()
        {
            ClearCacheInClassMetadata();
        }

        private void ClearCacheInClassMetadata()
        {
            var cache = GetCacheInClassMetadata();

            if (cache != null)
            {
                cache.Clear();
            }
        }

        private TypeCache<IClassMetadata> GetCacheInClassMetadata()
        {
            var cacheField = typeof(ClassMetadata).GetField("Cache", BindingFlags.NonPublic | BindingFlags.Static);

            if (cacheField == null)
            {
                throw new Exception("Cannot find private static field ClassMetadata.Cache.");
            }

            var cacheValue = cacheField.GetValue(null);

            if (cacheValue == null)
            {
                // cache has not been created yet so nothing to clear.
                return null;
            }

            return (TypeCache<IClassMetadata>)cacheValue;
        }

        [TestClass]
        public class Constructor : ClassMetadataTests
        {
            [TestMethod]
            public void ShouldThrowArgumentNullExceptionWhenTypeIsNull()
            {
                GWT.Given("testing constructor")
                    .When(x => new ClassMetadata(null))
                    .Then<ArgumentException>().ShouldBeThrown().ForParameter("type");
            }

            [TestMethod]
            public void ShouldSetTypePropertyToPropertyArgument()
            {
                // Given

                // When
                var metadata = new ClassMetadata(typeof(Exception));

                // Then
                metadata.Should().NotBeNull();
                metadata.Type.Should().Be(typeof(Exception));
            }
        }

        [TestClass]
        public class Get : ClassMetadataTests
        {
            [TestMethod]
            public void ShouldCreateAndReturnMetadataForClassThatHasNotBeGot()
            {
                // Given

                // When
                var metaData = ClassMetadata.Get<Exception>();

                // Then
                metaData.Should().NotBeNull();
            }

            [TestMethod]
            public void ShouldReturnMetadataFromCacheForClassThatHasBeenGot()
            {
                // Given
                var firstGet = ClassMetadata.Get<Exception>();

                // When
                var secondGet = ClassMetadata.Get<Exception>();

                // Then
                firstGet.Should().BeSameAs(secondGet);
            }
        }

        [TestClass]
        public class GetProperty_StaticMethod : ClassMetadataTests
        {
            [TestMethod]
            public void ShouldReturnMetadataForRequestedProperty()
            {
                // Given

                // When
                var propertyMetadata = ClassMetadata.GetProperty<Exception, string>(x => x.Message);

                // Then
                propertyMetadata.Should().NotBeNull();
                propertyMetadata.PropertyInfo.Name.Should().Be("Message");
            }
        }

        [TestClass]
        public class Properties : ClassMetadataTests
        {
            [TestMethod]
            public void ShouldReturnLazyCollectionOfIPropertyMetadata()
            {
                // Given
                var classMetadata = new ClassMetadata<Exception>();

                // When
                var properties = classMetadata.Properties;

                // Then
                properties.IsValueCreated.Should().BeFalse("because reflection is slow so we want a lazy collection");
                properties.Value.Any().Should().BeTrue("because System.Exception has public properties and probably has private properties");
            }

            [TestMethod]
            public void ShouldReturnLazyCollectionThatContainsPublicAndNotPublicProperties()
            {
                // Given
                var classMetadata = new ClassMetadata<Exception>();

                // When
                var properties = classMetadata.Properties;

                // Then
                properties.Value.Any(p => p.IsPublic).Should().BeTrue();
                properties.Value.Any(p => p.IsNotPublic).Should().BeTrue();
            }
        }

        [TestClass]
        public class GetProperty : ClassMetadataTests
        {
            [TestMethod]
            public void ShouldThrowArgumentExceptionWhenPropertyNameIsWhitespace()
            {
                GWT.Given(ClassMetadata.Get<Exception>())
                    .When(m => m.GetProperty(""))
                    .Then<ArgumentException>().ShouldBeThrown().ForParameter("propertyName");
            }

            [TestMethod]
            public void ShouldThrowArgumentExceptionWhenPropertyNameDoesNotExist()
            {
                GWT.Given(ClassMetadata.Get<Exception>())
                    .When(m => m.GetProperty("MissingPropertyName"))
                    .Then<ArgumentException>().ShouldBeThrown().ForParameter("propertyName");
            }

            [TestMethod]
            public void ShouldReturnMetadataForRequestedProperty()
            {
                // Given
                var propertyName = "Message";
                var classMetadata = ClassMetadata.Get<Exception>();

                // When
                var propertyMetadata = classMetadata.GetProperty(propertyName);

                // Then
                propertyMetadata.Should().NotBeNull();
                propertyMetadata.PropertyInfo.Name.Should().Be(propertyName);
            }
        }
    }
}
