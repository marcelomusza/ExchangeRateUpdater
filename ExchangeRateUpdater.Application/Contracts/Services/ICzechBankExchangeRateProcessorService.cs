using ExchangeRateUpdater.Application.DTOs;
using ExchangeRateUpdater.Application.DTOs.Enums;

namespace ExchangeRateUpdater.Application.Contracts.Services;

public interface ICzechBankExchangeRateProcessorService
{    
    Task<bool> ProcessExchangeRatesAsync(int bankId, DateTime date, Language language);
}
