using Benchmark.Calculator.Application.Contracts;

namespace Benchmark.Calculator.Application.Services
{
    public class CalculateService : ICalculateService
    {
        public (long, string) Add(string? numbers)
        {
            if (string.IsNullOrEmpty(numbers))
                return (0, "Empty String");

            if (numbers.Contains(",\n", StringComparison.InvariantCultureIgnoreCase)
                || numbers.Contains("\n,", StringComparison.InvariantCultureIgnoreCase))
                return (0, "Invalid Input");

            var delimiters = new string[] { ",", "\n" };

            var splitInput = numbers.Split(delimiters, StringSplitOptions.None);
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
