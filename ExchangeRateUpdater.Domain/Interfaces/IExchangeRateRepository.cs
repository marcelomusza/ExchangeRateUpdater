using ExchangeRateUpdater.Domain.Model;

namespace ExchangeRateUpdater.Domain.Interfaces;

public interface IExchangeRateRepository
{
    Task<bool> AddExchangeRatesAsync(IEnumerable<ExchangeRate> exchangeRates);

    Task<IEnumerable<ExchangeRate>> GetExchangeRatesByDayAsync(int bankId, DateTime date);

    Task<IEnumerable<ExchangeRate>> GetAllExchangeRatesAsync();
    Task<Bank> GetBankAsync(int bankId);
    Task<IEnumerable<Currency>> GetCurrenciesListAsync();
    Task<Currency> GetSourceCurrencyAsync(string sourceCurrency);
    Task<bool> HasRatesForDateAsync(int bankId, DateTime date);
    Task<IEnumerable<ExchangeRate>> GetExchangeRatesByBankAsync(int bankId);
}
