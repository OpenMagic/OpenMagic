using System.Collections;
using FluentAssertions;
using OpenMagic.Collections.Generic;
using Xunit;

namespace OpenMagic.Tests.Collections.Generic
{
    public class EnumerableBaseTests
    {
        public class Constructor
        {
            [Fact]
            public void CanConstruct()
            {
                // When
                var collection = new EnumerableBaseImplementation();

                // Then
                collection.Should().NotBeNull();
            }
        }

        public class EnumerableBaseImplementation : EnumerableBase<int>
        {
            public EnumerableBaseImplementation()
            {
                for (var i = 0; i < 10; i++)
                {
                    Items.Add(i);
                }
            }
        }

        public class GetEnumerator
        {
            [Fact]
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

        public class GetEnumerator_NonGeneric
        {
            [Fact]
            public void CanEnumerateAllValues()
            {
                // Given
                IEnumerable collection = new EnumerableBaseImplementation();

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
    }
}