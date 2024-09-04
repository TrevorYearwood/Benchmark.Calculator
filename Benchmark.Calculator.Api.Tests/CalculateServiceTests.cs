using Benchmark.Calculator.Application.Contracts;
using Benchmark.Calculator.Application.Services;

using Shouldly;

namespace Benchmark.Calculator.Api.Tests
{
    public class CalculateServiceTests
    {
        private ICalculateService? _calculateService;

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ShouldReturnZeroIfInputIsEmptyString(string? input)
        {
            //Arrange
            _calculateService = new CalculateService();

            //Act
            var (result, errors) = _calculateService.Add(input);

            //Assert 
            result.ShouldBe(0);
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("1,2", 3)]
        [InlineData("1,20,2", 23)]
        [InlineData("1,2233,2123,43441,111,234", 48143)]
        public void ShouldCorrectResultIfInputIsValidUsingCommaSeparatedValues(string? input, long correctResult)
        {
            //Arrange
            _calculateService = new CalculateService();

            //Act
            var (result, errors) = _calculateService.Add(input);

            //Assert 
            result.ShouldBe(correctResult);
        }

        [Theory]
        [InlineData("10\n", 10, "")]
        [InlineData("1,\n", 0, "Invalid Input")]
        [InlineData("1\n2, 3", 6, "")]
        public void ShouldCorrectResultIfInputIsValidUsingCommaSeparatedValuesAndNewLines(string? input, long expectedResult, string expectedErrors)
        {
            //Arrange
            _calculateService = new CalculateService();

            //Act
            var (result, errors) = _calculateService.Add(input);

            //Assert 
            result.ShouldBe(expectedResult);
            errors.ShouldBe(expectedErrors);
        }
    }
}