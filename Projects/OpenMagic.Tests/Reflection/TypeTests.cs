using OpenMagic.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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

            [TestMethod]
            public void ShouldThrowArgumentNullExceptionWhenValueIsNull()
            {
                // When
                Action action = () => Type<Exception>.Property<object>(null);

                // Then
                action.ShouldThrow<ArgumentNullException>()
                    .Subject.Message.EndsWith("Parameter name: value");
            }
        }
    }
}
