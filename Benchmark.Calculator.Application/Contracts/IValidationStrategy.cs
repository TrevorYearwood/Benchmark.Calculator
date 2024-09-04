namespace Benchmark.Calculator.Application.Contracts
{
    public interface IValidationStrategy
    {
        bool Validate(int input);
    }
}
