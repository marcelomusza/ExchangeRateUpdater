using ExchangeRateUpdater.Application.Contracts;
using ExchangeRateUpdater.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExchangeRateUpdater.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddHttpClient<ICzechBankExchangeRateService, CzechBankExchangeRateService>(client =>
        {
            client.BaseAddress = new Uri(configuration["CzechBankApi:BaseAddress"]!);
        });

        return services;
    }
}