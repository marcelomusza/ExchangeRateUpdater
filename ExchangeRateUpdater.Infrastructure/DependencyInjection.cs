using ExchangeRateUpdater.Application.Configurations;
using ExchangeRateUpdater.Application.Contracts;
using ExchangeRateUpdater.Infrastructure.Factories;
using ExchangeRateUpdater.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExchangeRateUpdater.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(DependencyInjection).Assembly;        

        //services.AddHttpClient("CzechBank", client =>
        //{
        //    client.BaseAddress = new Uri(bankApis["CzechBank:BaseAddress"]!);
        //});

        //services.AddHttpClient("ArgentinaBank", client =>
        //{
        //    client.BaseAddress = new Uri(bankApis["ArgentinaBank:BaseAddress"]!);
        //});

        services.Configure<Dictionary<string, BankApiConfig>>(configuration.GetSection("BankApis"));
        services.AddHttpClient();
        services.AddSingleton<IBankApiHttpClientFactory, BankApiHttpClientFactory>();

        services.AddTransient<ICzechBankExchangeRateService, CzechBankExchangeRateService>();

        return services;
    }
}