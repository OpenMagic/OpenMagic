using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using OpenMagic.Specifications.Helpers;
using Reqnroll;

namespace OpenMagic.Specifications.Steps;

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
        switch (typeName)
        {
            case "List<T>":
                return typeof(List<string>);

            case "Exception[]":
                return typeof(Exception[]);

            default:
                return Type.GetType("System." + typeName, true);
        }
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
        ((List<string>)_actual.Result).Any().Should().BeTrue();
    }

    [Then(@"the result should be an array of random number of items")]
    public void ThenTheResultShouldBeAnArrayOfRandomNumberOfItems()
    {
        ((Exception[])_actual.Result).Any().Should().BeTrue();
    }
}