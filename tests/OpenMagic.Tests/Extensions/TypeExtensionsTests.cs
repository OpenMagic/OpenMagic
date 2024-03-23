using System;
using System.Collections.Generic;
using FluentAssertions;
using OpenMagic.Extensions;
using Xunit;

namespace OpenMagic.Tests.Extensions;

public class TypeExtensionsTests
{
    public class IsEnumerableString
    {
        [Fact]
        public void ReturnsTrueWhenTypeIsIEnumerableOfString()
        {
            typeof(IEnumerable<string>).IsEnumerableString().Should().BeTrue();
        }

        [Fact]
        public void ReturnsFalseWhenTypeIsNotIEnumerableOfString()
        {
            typeof(int).IsEnumerableString().Should().BeFalse();
            typeof(DateTime).IsEnumerableString().Should().BeFalse();
            typeof(string).IsEnumerableString().Should().BeFalse();
        }
    }

    public class IsString
    {
        [Fact]
        public void ReturnsTrueWhenTypeIsString()
        {
            typeof(string).IsString().Should().BeTrue();
        }

        [Fact]
        public void ReturnsFalseWhenTypeIsNotString()
        {
            typeof(int).IsString().Should().BeFalse();
            typeof(DateTime).IsString().Should().BeFalse();
        }
    }
}