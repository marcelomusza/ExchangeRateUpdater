using ExchangeRateUpdater.Application.Configurations;
using ExchangeRateUpdater.Application.Contracts;
using ExchangeRateUpdater.Application.Contracts.External;
using ExchangeRateUpdater.Application.Services;
using ExchangeRateUpdater.Domain.Interfaces;
using ExchangeRateUpdater.Infrastructure.Data;
using ExchangeRateUpdater.Infrastructure.Factories;
using ExchangeRateUpdater.Infrastructure.Jobs;
using ExchangeRateUpdater.Infrastructure.Repositories;
using ExchangeRateUpdater.Infrastructure.Services.External;
using Microsoft.EntityFrameworkCore;
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

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        services.Configure<Dictionary<string, BankApiConfig>>(configuration.GetSection("BankApis"));
        services.AddHttpClient();
        services.AddSingleton<IBankApiHttpClientFactory, BankApiHttpClientFactory>();
        services.AddSingleton<IExchangeRateJobScheduler, ExchangeRateJobScheduler>();

        services.AddTransient<ICzechBankExchangeRateService, CzechBankExchangeRateService>();

        services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        

        return services;
    }
}