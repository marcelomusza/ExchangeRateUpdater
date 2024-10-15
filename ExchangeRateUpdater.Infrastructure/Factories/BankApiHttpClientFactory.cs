using ExchangeRateUpdater.Application.Configurations;
using Microsoft.Extensions.Options;

namespace ExchangeRateUpdater.Infrastructure.Factories;

public interface IBankApiHttpClientFactory
{
    HttpClient CreateClient(string bankType);
}

public class BankApiHttpClientFactory : IBankApiHttpClientFactory
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOptionsMonitor<Dictionary<string, BankApiConfig>> _bankApiConfigs;

    public BankApiHttpClientFactory(IHttpClientFactory httpClientFactory, IOptionsMonitor<Dictionary<string, BankApiConfig>> bankApiConfigs)
    {
        _httpClientFactory = httpClientFactory;
        _bankApiConfigs = bankApiConfigs;
    }

    public HttpClient CreateClient(string bankType)
    {
        var client = _httpClientFactory.CreateClient();
        if (_bankApiConfigs.CurrentValue.TryGetValue(bankType, out var config))
        {
            client.BaseAddress = new Uri(config.BaseAddress);
        }
        else
        {
            throw new ArgumentException($"No configuration found for bank type '{bankType}'");
        }
        return client;
    }
}
