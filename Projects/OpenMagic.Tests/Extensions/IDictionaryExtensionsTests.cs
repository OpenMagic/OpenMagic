using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenMagic.Extensions;

namespace OpenMagic.Tests.Extensions
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

        }
    }
}
