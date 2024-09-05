using Benchmark.Calculator.Application.Contracts;

using MediatR;

namespace Benchmark.Calculator.Application.Features.Calculator
{
    public class CalculatorHandler(ICalculateService calculateService) : IRequestHandler<CalculatorCommand, CalculatorResponse>
    {
        private readonly ICalculateService _calculateService = calculateService;

        public async Task<CalculatorResponse> Handle(CalculatorCommand command, CancellationToken cancellationToken)
        {
            var decodeNumbers = Uri.UnescapeDataString(command?.Numbers ?? string.Empty);
            decodeNumbers = decodeNumbers.Replace("\\n", "\n");

            var (result, errors) = _calculateService.Add(decodeNumbers);

            var response = new CalculatorResponse
            {
                Data = result,
                Errors = errors
            };

            return await Task.FromResult(response);
        }
    }
}
