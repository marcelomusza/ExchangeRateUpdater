using ExchangeRateUpdater.Application.DTOs.Enums;

namespace ExchangeRateUpdater.Application.Contracts.Services;

public interface ICzechBankExchangeRateProcessorService
{
    Task<bool> ProcessExchangeRatesAsync(DateTime date, Language language);
}
