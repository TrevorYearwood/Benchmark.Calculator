using Benchmark.Calculator.Application.Contracts;

namespace Benchmark.Calculator.Application.Services.Validation
{
    public class NegativeNumberValidation : IValidationStrategy
    {
        public bool Validate(int input)
        {
            if (input < 0)
                return false;

            return true;
        }
    }
}
