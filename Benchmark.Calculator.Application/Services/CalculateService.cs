using Benchmark.Calculator.Application.Contracts;
using Benchmark.Calculator.Application.Services.Validation;

namespace Benchmark.Calculator.Application.Services
{
    public class CalculateService : ICalculateService
    {
        private List<IValidationStrategy> _validationStrategies = [];

        private const string CustomDelimiter = "//";
        private const int MaxNumber = 1000;

        public (long, string) Add(string? numbers)
        {
            if (string.IsNullOrEmpty(numbers))
                return (0, "Empty String");

            if (numbers.Contains(",\n", StringComparison.InvariantCultureIgnoreCase)
                || numbers.Contains("\n,", StringComparison.InvariantCultureIgnoreCase))
                return (0, "Invalid Input");

            var numbersSpan = numbers.AsSpan();

            var delimiters = new List<string> { "\n" };

            if (numbers.StartsWith(CustomDelimiter))
            {
                var customDelimiter = numbersSpan.Slice(2, numbersSpan.IndexOf('\n') - 2);

                delimiters.Add(customDelimiter.ToString());
            }
            else
            {
                delimiters.Add(",");
            }

            var numbersArray = numbers.StartsWith(CustomDelimiter)
                                    ? numbersSpan.Slice(2).ToString()
                                    : numbers.ToString();

            var splitInput = numbersArray
                                .Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries);

            return AddNumbers(splitInput);
        }

        private (long, string) AddNumbers(string[] splitInput)
        {
            _validationStrategies.Add(new NegativeNumberValidation());
            _validationStrategies.Add(new MaxNumberValidationStrategy(MaxNumber));

            long sum = 0;
            int result = 0;
            var isValid = false;
            var negativeNumbers = new List<int>();

            foreach (var number in splitInput)
            {
                foreach (var strategy in _validationStrategies)
                {
                    try
                    {
                        result = int.Parse(number);
                        isValid = strategy.Validate(result);
                    }
                    catch
                    {
                        return (0, string.Empty);
                    }
                }

                if (result < 0)
                    negativeNumbers.Add(result);

                if (isValid)
                    sum += result;
            }

            if (negativeNumbers.Count > 0)
            {
                var negativeNumbersToString = negativeNumbers.Count > 0
                    ? string.Join(", ", negativeNumbers)
                    : negativeNumbers.ToString();

                return (0, $"Negatives not allowed : {negativeNumbersToString}");
            }

            return (sum, string.Empty);
        }
    }
}
