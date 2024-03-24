using System;
using FluentAssertions;
using OpenMagic.Reflection;
using Xunit;

namespace OpenMagic.Tests.Reflection;

public class TypeTests
{
    public class Property
    {
        [Fact]
        public void Should_Be_PropertyInfo_ForRequestedProperty()
        {
            // When
            var propertyInfo = Type<Exception>.Property(x => x.Message);

            // Then
            propertyInfo.Name.Should().Be("Message");
        }
    }
}