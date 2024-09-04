using Benchmark.Calculator.Application.Contracts;

namespace Benchmark.Calculator.Application.Services
{
    public class CalculateService : ICalculateService
    {
        public long Add(string? input)
        {
            if (string.IsNullOrEmpty(input))
                return 0;

            var splitInput = input.Split(',');
            long sum = 0;

            foreach (var number in splitInput)
            {
                if (int.TryParse(number, out int result))
                {
                    sum += result;
                }
            }

            return sum;
        }
    }
}
