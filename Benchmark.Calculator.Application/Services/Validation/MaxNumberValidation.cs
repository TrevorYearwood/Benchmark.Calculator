using Benchmark.Calculator.Application.Contracts;

namespace Benchmark.Calculator.Application.Services.Validation
{
    public class MaxNumberValidationStrategy(long maxValue) : IValidationStrategy
    {
        private readonly long _maxValue = maxValue;

        public bool Validate(long input)
        {
            if (input > _maxValue)
                return false;

            return true;
        }
    }
}