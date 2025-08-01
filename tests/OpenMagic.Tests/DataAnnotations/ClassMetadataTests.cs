﻿using System;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using OpenMagic.Collections.Generic;
using OpenMagic.DataAnnotations;
using Xunit;

namespace OpenMagic.Tests.DataAnnotations
{
    public class ClassMetadataTests
    {
        protected ClassMetadataTests()
        {
            ClearCacheInClassMetadata();
        }

        private static void ClearCacheInClassMetadata()
        {
            var cache = GetCacheInClassMetadata();

            cache?.Clear();
        }

        private static TypeCache<IClassMetadata> GetCacheInClassMetadata()
        {
            var cacheField = typeof(ClassMetadata).GetField("Cache", BindingFlags.NonPublic | BindingFlags.Static);

            if (cacheField == null)
            {
                throw new Exception("Cannot find private static field ClassMetadata.Cache.");
            }

            var cacheValue = cacheField.GetValue(null);

            return (TypeCache<IClassMetadata>)cacheValue;
        }

        public class Constructor : ClassMetadataTests
        {
            [Fact]
            public void ShouldThrowArgumentNullExceptionWhenTypeIsNull()
            {
                // ReSharper disable once ObjectCreationAsStatement
                // ReSharper disable once AssignNullToNotNullAttribute because this is a test for the exception
                Action action = () => new ClassMetadata(null);

                action.Should().Throw<ArgumentException>().And.ParamName.Should().Be("type");
            }

            [Fact]
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

        public class Get : ClassMetadataTests
        {
            [Fact]
            public void ShouldCreateAndReturnMetadataForClassThatHasNotBeGot()
            {
                // Given

                // When
                var metaData = ClassMetadata.Get<Exception>();

                // Then
                metaData.Should().NotBeNull();
            }

            [Fact]
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

        public class GetProperty : ClassMetadataTests
        {
            [Fact]
            public void ShouldThrowArgumentExceptionWhenPropertyNameIsWhitespace()
            {
                var metadata = ClassMetadata.Get<Exception>();

                Action action = () => metadata.GetProperty("");

                action.Should().Throw<ArgumentException>().And.ParamName.Should().Be("propertyName");
            }

            [Fact]
            public void ShouldThrowArgumentExceptionWhenPropertyNameDoesNotExist()
            {
                var metadata = ClassMetadata.Get<Exception>();

                Action action = () => metadata.GetProperty("MissingPropertyName");

                action.Should().Throw<ArgumentException>().And.ParamName.Should().Be("propertyName");
            }

            [Fact]
            public void ShouldReturnMetadataForRequestedProperty()
            {
                // Given
                const string propertyName = "Message";
                var classMetadata = ClassMetadata.Get<Exception>();

                // When
                var propertyMetadata = classMetadata.GetProperty(propertyName);

                // Then
                propertyMetadata.Should().NotBeNull();
                propertyMetadata.PropertyInfo.Name.Should().Be(propertyName);
            }
        }

        // ReSharper disable once InconsistentNaming
        public class GetProperty_StaticMethod : ClassMetadataTests
        {
            [Fact]
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

        public class Properties : ClassMetadataTests
        {
            [Fact]
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

            [Fact]
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
    }
}