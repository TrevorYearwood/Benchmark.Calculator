namespace Benchmark.Calculator.Application.Contracts
{
    public interface IValidationStrategy
    {
        bool Validate(long input);
    }
}
