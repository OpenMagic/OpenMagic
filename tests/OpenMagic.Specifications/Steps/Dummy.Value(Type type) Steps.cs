using System;
using System.Collections.Generic;
using FluentAssertions;
using OpenMagic.Specifications.Helpers;
using Reqnroll;

namespace OpenMagic.Specifications.Steps
{
    [Binding]
// ReSharper disable once InconsistentNaming
    public class Dummy_Value_Type_type_Steps
    {
        private readonly ActualData _actual;
        private readonly GivenData _given;

        public Dummy_Value_Type_type_Steps(GivenData given, ActualData actual)
        {
            _given = given;
            _actual = actual;
        }

        [Given(@"type is (.*)")]
        public void GivenTypeIs(string givenType)
        {
            _given.Type = GetTypeFromTypeName(givenType);
        }

        private static Type GetTypeFromTypeName(string typeName)
        {
            return typeName switch
            {
                "List<T>" => typeof(List<string>),
                "Exception[]" => typeof(Exception[]),
                _ => Type.GetType("System." + typeName, true)
            };
        }

        [When(@"Dummy\.Value\(type\) is called")]
        public void WhenDummy_ValueTypeIsCalled()
        {
            _actual.Result = new Dummy().Value(_given.Type);
        }

        [Then(@"the type of the result should be (.*)")]
        public void ThenTheTypeOfTheResultShouldBe(string expectedResultType)
        {
            _actual.Result.Should().BeOfType(GetTypeFromTypeName(expectedResultType));
        }

        [Then(@"the result should be a list of random number of items")]
        public void ThenTheResultShouldBeAListOfRandomNumberOfItems()
        {
            var actualResult = (List<string>)_actual.Result;

            actualResult.Count.Should().BeGreaterThan(0, "the list should contain a random number of items");
        }

        [Then(@"the result should be an array of random number of items")]
        public void ThenTheResultShouldBeAnArrayOfRandomNumberOfItems()
        {
            var actualResult = (Exception[])_actual.Result;

            actualResult.Should().NotBeNull();
            actualResult.Length.Should().BeGreaterThan(0, "the array should contain a random number of items");
        }
    }
}