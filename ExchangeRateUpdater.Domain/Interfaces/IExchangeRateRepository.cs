using ExchangeRateUpdater.Domain.Model;

namespace ExchangeRateUpdater.Domain.Interfaces;

public interface IExchangeRateRepository
{
    Task<bool> AddExchangeRatesAsync(IEnumerable<ExchangeRate> exchangeRates);

    Task<ExchangeRate> GetExchangeRatesByDayAsync(DateTime date);

    Task<IEnumerable<ExchangeRate>> GetAllExchangeRatesAsync();
    Task<Bank> GetOrCreateBankAsync(string bankName);
    Task<IEnumerable<Currency>> GetCurrenciesListAsync();
    Task<Currency> GetSourceCurrencyAsync(string sourceCurrency);
    Task<bool> HasRatesForDateAsync(DateTime date);
}
