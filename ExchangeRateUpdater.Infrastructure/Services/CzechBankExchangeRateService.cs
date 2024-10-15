using AutoMapper;
using ExchangeRateUpdater.Application.Contracts;
using ExchangeRateUpdater.Application.DTOs;
using ExchangeRateUpdater.Application.DTOs.Enums;
using ExchangeRateUpdater.Domain.Entities;
using ExchangeRateUpdater.Infrastructure.Factories;
using System.Net.Http.Json;
using System.Text.Json;

namespace ExchangeRateUpdater.Infrastructure.Services;

public class CzechBankExchangeRateService : ICzechBankExchangeRateService
{
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;

    public CzechBankExchangeRateService(IBankApiHttpClientFactory httpClientFactory, IMapper mapper)
    {
        _httpClient = httpClientFactory.CreateClient("CzechBank");
        _mapper = mapper;
    }

    public async Task<IEnumerable<CzechBankExchangeRateDto>> GetExchangeRatesAsync(DateTime date, Language language)
    {
        var response = await _httpClient.GetAsync($"?date={ date.ToString("yyyy-MM-dd") }&lang={ language.ToString() }");

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException("Failed to retrieve exchange rates from the Czech Bank public Api");
        }

        var exchangeRateResponse = await response.Content.ReadFromJsonAsync<ExchangeRatesResponse>();
        var exchangeRates = exchangeRateResponse?.Rates;

        return _mapper.Map<IEnumerable<CzechBankExchangeRateDto>>(exchangeRates);
    }
}
