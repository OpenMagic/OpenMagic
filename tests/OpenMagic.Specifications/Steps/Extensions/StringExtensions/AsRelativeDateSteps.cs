using System;
using FluentAssertions;
using OpenMagic.Extensions;
using OpenMagic.Specifications.Helpers;
using Reqnroll;

namespace OpenMagic.Specifications.Steps.Extensions.StringExtensions
{
    [Binding]
    [Scope(Feature = "AsRelativeDate")]
    public class AsRelativeDateSteps(ActualData actual)
    {
        private string _input;

        [Given("date string is {string}")]
        public void GivenDateStringIs(string dateString)
        {
            _input = dateString;
        }

        [When("I call AsRelativeDate")]
        public void WhenICallAsRelativeDate()
        {
            actual.GetResult(() => _input.AsRelativeDate());
        }

        [Then("{string} should be returned")]
        public void ThenShouldBeReturned(string expected)
        {
            actual.Exception.Should().BeNull();

            var actualResult = (DateTime)actual.Result;

            switch (expected)
            {
                case "DateTime.MaxValue":
                    actualResult.Should().Be(DateTime.MaxValue.Date);
                    break;

                case "DateTime.MinValue":
                    actualResult.Should().Be(DateTime.MinValue.Date);
                    break;

                case "DateTime.UtcNow":
                    actualResult.Should().Be(DateTime.UtcNow.Date);
                    break;

                case "16 July 2024":
                    actualResult.Should().Be(new DateTime(2024, 7, 16));
                    break;

                case "today":
                    actualResult.Should().Be(DateTime.UtcNow.Date);
                    break;

                case "tomorrow":
                    actualResult.Should().Be(DateTime.UtcNow.Date.AddDays(1));
                    break;

                case "yesterday":
                    actualResult.Should().Be(DateTime.UtcNow.Date.AddDays(-1));
                    break;

                case "today + 2 days":
                    actualResult.Should().Be(DateTime.UtcNow.Date.AddDays(2));
                    break;

                case "today - 2 days":
                    actualResult.Should().Be(DateTime.UtcNow.Date.AddDays(-2));
                    break;

                case "next month":
                    actualResult.Should().Be(DateTime.UtcNow.Date.AddMonths(1));
                    break;

                case "last month":
                    actualResult.Should().Be(DateTime.UtcNow.Date.AddMonths(-1));
                    break;

                case "next month and 1 day":
                    actualResult.Should().Be(DateTime.UtcNow.Date.AddMonths(1).AddDays(1));
                    break;

                case "last month and 1 day":
                    actualResult.Should().Be(DateTime.UtcNow.Date.AddMonths(-1).AddDays(-1));
                    break;

                case "next month and 2 days":
                    actualResult.Should().Be(DateTime.UtcNow.Date.AddMonths(1).AddDays(2));
                    break;

                case "last month and 2 days":
                    actualResult.Should().Be(DateTime.UtcNow.Date.AddMonths(-1).AddDays(-2));
                    break;


                case "plus 2 months and 2 days":
                    actualResult.Should().Be(DateTime.UtcNow.Date.AddMonths(2).AddDays(2));
                    break;

                case "minus 2 months and 2 days":
                    actualResult.Should().Be(DateTime.UtcNow.Date.AddMonths(-2).AddDays(-2));
                    break;

                case "last year":
                    actualResult.Should().Be(DateTime.UtcNow.Date.AddYears(-1));
                    break;

                case "next year":
                    actualResult.Should().Be(DateTime.UtcNow.Date.AddYears(+1));
                    break;

                default:
                    throw new NotSupportedException($"Expected value '{expected}' is not supported.");
            }
        }
    }
}