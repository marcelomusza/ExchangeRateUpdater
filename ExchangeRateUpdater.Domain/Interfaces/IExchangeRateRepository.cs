using ExchangeRateUpdater.Domain.Model;

namespace ExchangeRateUpdater.Domain.Interfaces;

public interface IExchangeRateRepository
{
    Task<bool> AddExchangeRatesAsync(IEnumerable<ExchangeRate> exchangeRates);

    Task<ExchangeRate> GetExchangeRatesByDayAsync(DateTime date);

    Task<IEnumerable<ExchangeRate>> GetAllExchangeRatesAsync();
    
}
