using System;
using FluentAssertions;
using OpenMagic.Collections.Generic;
using Xunit;

namespace OpenMagic.Tests.Collections.Generic
{
    public class TypeCacheTests
    {
        public class Get
        {
            [Fact]
            public void ShouldReturnResultOfValueFactoryWhenTypeIsNotInCache()
            {
                // Given
                var cache = new TypeCache<object>();

                // When
                var value = cache.Get<string>(() => "value");

                // Then
                value.Should().Be("value");
            }

            [Fact]
            public void ShouldReturnResultValueFromDictionaryWhenTypeIsInCache()
            {
                // Given
                var cache = new TypeCache<Exception>();
                var initialValue = cache.Get<ArgumentNullException>(() => new ArgumentNullException());

                // When
                var value = cache.Get<ArgumentNullException>(() => new ArgumentNullException());

                // Then
                value.Should().BeSameAs(initialValue);
            }
        }
    }
}