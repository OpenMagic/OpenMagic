using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenMagic.Collections.Generic;
using FluentAssertions;

namespace OpenMagic.Tests.Collections.Generic
{
    public class EnumerableBaseTests
    {
        [TestClass]
        public class Constructor
        {
            [TestMethod]
            public void CanConstruct()
            {
                // When
                var collection = new EnumerableBaseImplementation();

                // Then
                collection.Should().NotBeNull();
            }
        }

        [TestClass]
        public class GetEnumerator
        {
            [TestMethod]
            public void CanEnumerateAllValues()
            {
                // Given
                var collection = new EnumerableBaseImplementation();

                // When
                var enumerator = collection.GetEnumerator();

                // Then
                var i = -1;
                while (enumerator.MoveNext())
                {
                    enumerator.Current.Should().Be(++i, "because collection should be 0 ... 9");
                }

                i.Should().Be(9, "because the last item in collection should be 9");
            }
        }

        public class EnumerableBaseImplementation : EnumerableBase<int>
        {
            public EnumerableBaseImplementation()
                : base()
            {
                for (int i = 0; i < 10; i++)
                {
                    this.Items.Add(i);
                }
            }
        }
    }
}
