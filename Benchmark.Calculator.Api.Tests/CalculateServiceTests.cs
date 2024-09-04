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
        [InlineData("1,223,212,434,111,234", 1215)]
        public void ShouldShowCorrectResultIfInputIsValidUsingCommaSeparatedValues(string? input, long correctResult)
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
        public void ShouldShowCorrectResultIfInputIsValidUsingCommaSeparatedValuesAndNewLines(string? input, long expectedResult, string expectedErrors)
        {
            //Arrange
            _calculateService = new CalculateService();

            //Act
            var (result, errors) = _calculateService.Add(input);

            //Assert 
            result.ShouldBe(expectedResult);
            errors.ShouldBe(expectedErrors);
        }

        [Theory]
        [InlineData("//;\n1;2", 3, "")]
        [InlineData("//*\n14*22*200", 236, "")]
        [InlineData("//'\n1'78\n90'3", 172, "")]
        public void ShouldShowCorrectResultIfInputIsValidUsingCustomDelimiter(string? input, long expectedResult, string expectedErrors)
        {
            //Arrange
            _calculateService = new CalculateService();

            //Act
            var (result, errors) = _calculateService.Add(input);

            //Assert 
            result.ShouldBe(expectedResult);
            errors.ShouldBe(expectedErrors);
        }

        [Theory]
        [InlineData("//;\n-1;2;-23", 0, "Negatives not allowed : -1, -23")]
        [InlineData("//*\n14*-22*-200\n-99*-45", 0, "Negatives not allowed : -22, -200, -99, -45")]
        [InlineData("1,78\n-88,3", 0, "Negatives not allowed : -88")]
        public void ShouldShowCorrectResultIfInputIsHasNegativeNumber(string? input, long expectedResult, string expectedErrors)
        {
            //Arrange
            _calculateService = new CalculateService();

            //Act
            var (result, errors) = _calculateService.Add(input);

            //Assert 
            result.ShouldBe(expectedResult);
            errors.ShouldBe(expectedErrors);
        }

        [Theory]
        [InlineData("//;\n1002;2;23", 25, "")]
        [InlineData("//*\n1400*22*200\n9999*45", 267, "")]
        [InlineData("1,78\n8888,3", 82, "")]
        public void ShouldShowCorrectResultIfInputIsNumbersGreaterThan1000(string? input, long expectedResult, string expectedErrors)
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