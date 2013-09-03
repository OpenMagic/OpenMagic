using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenMagic.Collections.Generic;

namespace OpenMagic.Tests.Collections.Generic
{
    public class TypeCacheTests
    {
        [TestClass]
        public class Get
        {
            [TestMethod]
            public void ShouldReturnResultOfValueFactoryWhenTypeIsNotInCache()
            {
                // Given
                var cache = new TypeCache<object>();

                // When
                var value = cache.Get<string>(() => "value");

                // Then
                value.Should().Be("value");
            }

            [TestMethod]
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
