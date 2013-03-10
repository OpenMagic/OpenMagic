using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenMagic.Extensions;

namespace OpenMagic.Tests.Extensions {
    [TestClass]
    public class TypeExtensions {

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
