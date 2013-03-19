using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenMagic.Extensions;

namespace OpenMagic.Tests.Extensions {
    [TestClass]
    public class TypeExtensions {

        [TestClass]
        public class IsEnumerableString {

            [TestMethod]
            public void ReturnsTrueWhenTypeIsIEnumerableOfString() {
                typeof(IEnumerable<string>).IsEnumerableString().Should().BeTrue();
            }

            [TestMethod]
            public void ReturnsFalseWhenTypeIsNotIEnumerableOfString() {
                typeof(int).IsEnumerableString().Should().BeFalse();
                typeof(DateTime).IsEnumerableString().Should().BeFalse();
            }
        }

        [TestClass]
        public class IsString {

            [TestMethod]
            public void ReturnsTrueWhenTypeIsString() {
                typeof(string).IsString().Should().BeTrue();
            }

            [TestMethod]
            public void ReturnsFalseWhenTypeIsNotString() {
                typeof(int).IsString().Should().BeFalse();
                typeof(DateTime).IsString().Should().BeFalse();                
            }
        }
    }
}
