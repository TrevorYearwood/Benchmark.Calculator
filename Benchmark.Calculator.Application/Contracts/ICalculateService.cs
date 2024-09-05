namespace Benchmark.Calculator.Application.Contracts
{
    public interface ICalculateService
    {
        (long, string) Add(string? numbers);
    }
}