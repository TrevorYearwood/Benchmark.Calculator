
using Benchmark.Calculator.Application.Contracts;

using MediatR;

namespace Benchmark.Calculator.Application.Features.Calculator
{
    public class CalculatorHandler : IRequestHandler<CalculatorCommand, CalculatorResponse>
    {
        private readonly ICalculateService _calculateService;

        public CalculatorHandler(ICalculateService calculateService)
        {
            _calculateService = calculateService;
        }

        public async Task<CalculatorResponse> Handle(CalculatorCommand command, CancellationToken cancellationToken)
        {
            var result = _calculateService.Add(command.Numbers);

            var response = new CalculatorResponse
            {
                Data = result
            };

            return await Task.FromResult(response);
        }
    }
}
