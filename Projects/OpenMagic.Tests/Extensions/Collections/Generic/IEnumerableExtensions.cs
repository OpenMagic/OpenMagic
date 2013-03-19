using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenMagic.Extensions.Collections.Generic;

namespace OpenMagic.Tests.Extensions.Collections.Generic {
    [TestClass]
    public class IEnumerableExtensions {

        [TestClass]
        public class IsNullOrEmpty {

            [TestMethod]
            public void ReturnsTrueWhenValueIsNull() {

                IEnumerable<string> nullString = null;
                IEnumerable<int> nullInt = null;

                OpenMagic.Extensions.Collections.Generic.IEnumerableExtensions.IsNullOrEmpty(nullString).Should().BeTrue();
                OpenMagic.Extensions.Collections.Generic.IEnumerableExtensions.IsNullOrEmpty(nullInt).Should().BeTrue();
            }

            [TestMethod]
            public void ReturnsTrueWhenValueIsHasZeroElements() {

                var emptyString = new List<string>();
                var emptyInt = new List<int>();

                OpenMagic.Extensions.Collections.Generic.IEnumerableExtensions.IsNullOrEmpty(emptyString).Should().BeTrue();
                OpenMagic.Extensions.Collections.Generic.IEnumerableExtensions.IsNullOrEmpty(emptyInt).Should().BeTrue();
            }

            [TestMethod]
            public void ReturnsFalseWhenValueHasElements() {

                var enumerableString = new List<string>() {"a", "b"};
                var enumerableInt = new List<int>() {1,2};

                OpenMagic.Extensions.Collections.Generic.IEnumerableExtensions.IsNullOrEmpty(enumerableString).Should().BeFalse();
                OpenMagic.Extensions.Collections.Generic.IEnumerableExtensions.IsNullOrEmpty(enumerableInt).Should().BeFalse();
            }
        }
    }
}
