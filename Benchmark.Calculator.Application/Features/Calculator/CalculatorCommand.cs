using MediatR;

namespace Benchmark.Calculator.Application.Features.Calculator
{
    public class CalculatorCommand : IRequest<CalculatorResponse>
    {
        public string? Numbers { get; set; }
    }
}
