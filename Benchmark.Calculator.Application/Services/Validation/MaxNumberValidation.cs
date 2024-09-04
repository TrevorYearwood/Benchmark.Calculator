using Benchmark.Calculator.Application.Contracts;

public class MaxNumberValidationStrategy : IValidationStrategy
{
    private readonly int _maxValue;

    public MaxNumberValidationStrategy(int maxValue)
    {
        _maxValue = maxValue;
    }

    public bool Validate(int input)
    {
        if (input > _maxValue)
            return false;

        return true;
    }
}
