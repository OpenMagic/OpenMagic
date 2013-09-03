using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenMagic.Collections.Generic;

namespace OpenMagic.Tests.Collections.Generic
{
    public class CacheTests
    {
        [TestClass]
        public class Get
        {
            [TestMethod]
            public void ShouldReturnResultOfValueFactoryWhenTypeIsNotInCache()
            {
                // Given
                var cache = new Cache<string, string>();

                // When
                var value = cache.Get("a", () => "value");

                // Then
                value.Should().Be("value");
            }

            [TestMethod]
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
