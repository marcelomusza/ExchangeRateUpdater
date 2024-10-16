using ExchangeRateUpdater.Application.Behaviors;
using ExchangeRateUpdater.Application.Contracts.Services;
using ExchangeRateUpdater.Application.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ExchangeRateUpdater.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;


        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(assembly));
        services.AddValidatorsFromAssembly(assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddAutoMapper(assembly);

        services.AddScoped<ICzechBankExchangeRateProcessorService, CzechBankExchangeRateProcessorService>();

        return services;
    }
}
