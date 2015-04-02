using OpenMagic.Specifications.Helpers;
using TechTalk.SpecFlow;

namespace OpenMagic.Specifications.Steps.ArgumentSteps
{
    [Binding]
    public class MustNotBeEmptySteps
    {
        private readonly GivenData _given;
        private readonly ActualData _actual;

        public MustNotBeEmptySteps(GivenData given, ActualData actual)
        {
            _given = given;
            _actual = actual;
        }

        [Given(@"param is an array with elements")]
        public void GivenParamIsAnArrayWithElements()
        {
            _given.Param = new[] { 1, 2, 3 };
        }

        [Given(@"param is an array with zero elements")]
        public void GivenParamIsAnArrayWithZeroElements()
        {
            _given.Param = new int[] { };
        }

        [When(@"I call Argument\.MustNotBeEmpty\(<param>, <paramName>\)")]
        public void WhenICallArgument_MustNotBeEmpty()
        {
            _actual.GetResult(() => ((int[])_given.Param).MustNotBeEmpty("dummy"));
        }
    }
}
