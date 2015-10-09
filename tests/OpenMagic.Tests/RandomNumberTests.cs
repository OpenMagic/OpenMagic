using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace OpenMagic.Tests
{
    public class RandomNumberTests
    {
        public class NextByte : RandomNumber<byte>
        {
            public NextByte()
                : base(RandomNumber.NextByte, RandomNumber.NextByte, 0, 100, (int)((byte.MaxValue - byte.MinValue)*.9))
            {
            }
        }

        public class NextSByte : RandomNumber<sbyte>
        {
            public NextSByte()
                : base(
                    RandomNumber.NextSByte, RandomNumber.NextSByte, 0, 100, (int)((sbyte.MaxValue - sbyte.MinValue)*.9))
            {
            }
        }

        public class NextChar : RandomNumber<char>
        {
            public NextChar() : base(RandomNumber.NextChar, RandomNumber.NextChar, '0', (char)(char.MinValue + 100))
            {
            }
        }

        public class NextDecimal : RandomNumber<decimal>
        {
            public NextDecimal() : base(RandomNumber.NextDecimal, RandomNumber.NextDecimal, -50, 50)
            {
            }
        }

        public class NextDouble : RandomNumber<double>
        {
            public NextDouble() : base(RandomNumber.NextDouble, RandomNumber.NextDouble, -50, 50)
            {
            }
        }

        public class NextFloat : RandomNumber<float>
        {
            public NextFloat() : base(RandomNumber.NextFloat, RandomNumber.NextFloat, -50, 50)
            {
            }
        }

        public class NextInt : RandomNumber<int>
        {
            public NextInt() : base(RandomNumber.NextInt, RandomNumber.NextInt, -50, 50)
            {
            }
        }

        public class NextUInt : RandomNumber<uint>
        {
            public NextUInt() : base(RandomNumber.NextUInt, RandomNumber.NextUInt, 0, 100)
            {
            }
        }

        public class NextLong : RandomNumber<long>
        {
            public NextLong() : base(RandomNumber.NextLong, RandomNumber.NextLong, -50, 50)
            {
            }
        }

        public class NextULong : RandomNumber<ulong>
        {
            public NextULong() : base(RandomNumber.NextULong, RandomNumber.NextULong, 0, 100)
            {
            }
        }

        public class NextShort : RandomNumber<short>
        {
            public NextShort() : base(RandomNumber.NextShort, RandomNumber.NextShort, -50, 50)
            {
            }
        }

        public class NextUShort : RandomNumber<ushort>
        {
            public NextUShort() : base(RandomNumber.NextUShort, RandomNumber.NextUShort, 0, 100)
            {
            }
        }

        public abstract class RandomNumber<T> where T : IComparable<T>
        {
            private readonly int _expectedUniqueRandomNumbers;
            private readonly T _maxValue;
            private readonly T _minValue;
            private readonly Func<T> _randomNumberFactory;
            private readonly Func<T, T, T> _randomNumberFactoryWithRange;

            protected RandomNumber(
                Func<T> randomNumberFactory,
                Func<T, T, T> randomNumberFactoryWithRange,
                T minValue,
                T maxValue,
                int expectedUniqueRandomNumbers = 900)
            {
                _randomNumberFactory = randomNumberFactory;
                _randomNumberFactoryWithRange = randomNumberFactoryWithRange;
                _minValue = minValue;
                _maxValue = maxValue;
                _expectedUniqueRandomNumbers = expectedUniqueRandomNumbers;
            }

            [Fact]
            public void ShouldGetRandomValues()
            {
                // When
                var randomNumbers = RandomNumber.Enumerable(1000, _randomNumberFactory);

                // Then
                var uniqueValues =
                    from number in randomNumbers
                    group number by number
                    into g
                    select new {Number = g};

                uniqueValues.Count()
                    .Should()
                    .BeGreaterThan(_expectedUniqueRandomNumbers,
                        $"because there should be more than {_expectedUniqueRandomNumbers} random numbers");
            }

            [Fact]
            public void ShouldGetRandomValuesWithinDefinedRange()
            {
                // When
                var randomNumbers = RandomNumber.Enumerable(1000,
                    () => _randomNumberFactoryWithRange(_minValue, _maxValue));

                // Then
                randomNumbers.Count(n => n.CompareTo(_minValue) < 0 || n.CompareTo(_maxValue) >= 0).Should().Be(0);
            }
        }
    }
}