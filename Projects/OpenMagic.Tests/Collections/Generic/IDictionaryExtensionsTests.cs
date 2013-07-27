using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenMagic.Collections.Generic;

namespace OpenMagic.Tests.Collections.Generic
{
    public class IDictionaryExtensionsTests
    {
        [TestClass]
        public class FindValue
        {
            [TestMethod]
            public void ShouldReturnValueIfKeyIsInDictionary()
            {
                // Given
                var dictionary = new Dictionary<int, string>();

                dictionary.Add(1, "a");
                dictionary.Add(2, "b");

                // When / Then
                dictionary.FindValue(1).Should().Be("a");
                dictionary.FindValue(2).Should().Be("b");
            }

            [TestMethod]
            public void ShouldReturnNullIfKeyIsNotDictionary()
            {
                // Given
                var dictionary = new Dictionary<int, string>();

                dictionary.Add(1, "a");

                // When / Then
                dictionary.FindValue(2).Should().BeNull();
            }

            [TestMethod]
            public void ShouldThrowArgumentNullExceptionWhen_key_IsNull()
            {
                // Given
                var dictionary = new Dictionary<string, string>();

                // When
                Action action = () => dictionary.FindValue(null);

                // Then
                action.ShouldThrow<ArgumentNullException>().And.ParamName.Should().Be("key");
            }
        }
    }
}
