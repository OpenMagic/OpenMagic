using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenMagic.Reflection;

namespace OpenMagic.Tests.Reflection
{
    public class TypeTests
    {
        [TestClass]
        public class Property
        {
            [TestMethod]
            public void Should_Be_PropertyInfo_ForRequestedProperty()
            {
                // When
                var propertyInfo = Type<Exception>.Property(x => x.Message);

                // Then
                propertyInfo.Name.Should().Be("Message");
            }
        }
    }
}
