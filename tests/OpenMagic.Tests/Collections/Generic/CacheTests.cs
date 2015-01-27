using System;
using FluentAssertions;
using OpenMagic.Collections.Generic;
using Xunit;

namespace OpenMagic.Tests.Collections.Generic
{
    public class CacheTests
    {
        public class Get
        {
            [Fact]
            public void ShouldReturnResultOfValueFactoryWhenTypeIsNotInCache()
            {
                // Given
                var cache = new Cache<string, string>();

                // When
                var value = cache.Get("a", () => "value");

                // Then
                value.Should().Be("value");
            }

            [Fact]
            public void ShouldReturnResultValueFromDictionaryWhenTypeIsInCache()
            {
                // Given
                var cache = new Cache<Type, Exception>();
                var initialValue = cache.Get(typeof(ArgumentNullException), () => new ArgumentNullException());

                // When
                var value = cache.Get(typeof(ArgumentNullException), () => new ArgumentNullException());

                // Then
                value.Should().BeSameAs(initialValue);
            }
        }
    }
}