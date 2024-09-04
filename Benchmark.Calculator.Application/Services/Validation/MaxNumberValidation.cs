using Benchmark.Calculator.Application.Contracts;

namespace Benchmark.Calculator.Application.Services.Validation
{
    public class MaxNumberValidationStrategy(int maxValue) : IValidationStrategy
    {
        private readonly int _maxValue = maxValue;

        public bool Validate(int input)
        {
            if (input > _maxValue)
                return false;

            return true;
        }
    }
}