using System;
using FluentAssertions;
using OpenMagic.Specifications.Helpers;
using Reqnroll;

namespace OpenMagic.Specifications.Steps.ArgumentSteps;

[Binding]
public class MustBeBetweenSteps
{
    private readonly ActualData _actual;
    private readonly GivenData _given;

    public MustBeBetweenSteps(GivenData given, ActualData actual)
    {
        _given = given;
        _actual = actual;
    }

    [Given(@"value is (.*)")]
    public void GivenValueIs(int value)
    {
        _given.ParameterValue = value;
    }

    [Given(@"minimumValue is (.*)")]
    public void GivenMinimumValueIs(int minimumValue)
    {
        _given.MinimumInt = minimumValue;
    }

    [Given(@"maximumValue is (.*)")]
    public void GivenMaximumValueIs(int maximumValue)
    {
        _given.MaximumInt = maximumValue;
    }

    [When(@"I call Argument.MustBetween\(value, minimumValue, maximumValue\)")]
    public void WhenICall_Argument_MustBetween_Value_MinimumValue_MaximumValue()
    {
        try
        {
            // ReSharper disable once InvokeAsExtensionMethod
            _actual.Result = Argument.MustBeBetween((int)_given.ParameterValue, _given.MinimumInt, _given.MaximumInt, "value");
        }
        catch (Exception exception)
        {
            _actual.Exception = exception;
        }
    }

    [Then(@"number (.*) should be returned")]
    public void ThenShouldBeReturned(int expectedValue)
    {
        _actual.Exception.Should().BeNull();
        _actual.Result.Should().Be(expectedValue);
    }

    [Then(@"ArgumentOutOfRangeException should be thrown")]
    public void ThenArgumentOutOfRangeExceptionShouldBeThrown()
    {
        _actual.Exception.Should().BeOfType<ArgumentOutOfRangeException>();
    }
}