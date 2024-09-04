using Benchmark.Calculator.Application.Contracts;

namespace Benchmark.Calculator.Application.Services
{
    public class CalculateService : ICalculateService
    {
        private const string CustomDelimiter = "//";

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

        private static (long, string) AddNumbers(string[] splitInput)
        {
            long sum = 0;

            foreach (var number in splitInput)
            {
                try
                {
                    int result = int.Parse(number);
                    sum += result;
                }
                catch
                {
                    return (0, string.Empty);
                }
            }

            return (sum, string.Empty);
        }
    }
}
