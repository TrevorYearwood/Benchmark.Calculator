using Benchmark.Calculator.Application.Contracts;
using Benchmark.Calculator.Application.Services;

using Microsoft.Extensions.DependencyInjection;

namespace Benchmark.Calculator.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

            services.AddScoped<ICalculateService, CalculateService>();

            return services;
        }
    }
}
