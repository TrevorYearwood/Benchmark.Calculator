using Benchmark.Calculator.Application.Contracts;
using Benchmark.Calculator.Application.Services.Validation;

namespace Benchmark.Calculator.Application.Services
{
    public class CalculateService : ICalculateService
    {
        private readonly List<IValidationStrategy> _validationStrategies = [];

        private const string _customDelimiter = "//";
        private const int _maxNumber = 1000;

        public CalculateService()
        {
            _validationStrategies =
            [
                new NegativeNumberValidation(),
                new MaxNumberValidationStrategy(_maxNumber),
            ];
        }

        public (long, string) Add(string? numbers)
        {
            if (string.IsNullOrEmpty(numbers))
                return (0, "Empty String");

            var numbersSpan = numbers.AsSpan();

            List<string> delimiters = CreateDelimiters(numbers, numbersSpan);

            var numbersArray = numbers.StartsWith(_customDelimiter)
                        ? numbersSpan[numbersSpan.IndexOf('\n')..].ToString()
                        : numbers.ToString();

            foreach (var delimiter in delimiters)
            {
                if (numbersArray.Contains($"{delimiter}\n", StringComparison.InvariantCultureIgnoreCase)
                    || numbersArray.Contains($"\n{delimiter}", StringComparison.InvariantCultureIgnoreCase))
                    return (0, "Invalid Input");
            }

            var splitInput = numbersArray
                                .Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries);

            return AddNumbers(splitInput);
        }

        private static List<string> CreateDelimiters(string numbers, ReadOnlySpan<char> numbersSpan)
        {
            var delimiters = new List<string> { "\n" };

            if (numbers.StartsWith(_customDelimiter))
            {
                var customDelimiters = numbersSpan[_customDelimiter.Length..numbersSpan.IndexOf('\n')].ToArray();

                foreach (char delimiter in customDelimiters)
                {
                    delimiters.Add(delimiter.ToString()!);
                }
            }
            else
            {
                delimiters.Add(",");
            }

            return delimiters;
        }

        private (long, string) AddNumbers(string[] splitInput)
        {
            long sum = 0;
            long result = 0;
            var isValid = false;
            var negativeNumbers = new List<long>();

            foreach (var number in splitInput)
            {
                foreach (var strategy in _validationStrategies)
                {
                    try
                    {
                        result = long.Parse(number);
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

            return CheckNegativeNumbers(negativeNumbers, sum);
        }

        private static (long, string) CheckNegativeNumbers(List<long> negativeNumbers, long sum)
        {
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
