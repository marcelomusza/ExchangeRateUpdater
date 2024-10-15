using ExchangeRateUpdater.Application.DTOs;
using ExchangeRateUpdater.Application.DTOs.Enums;

namespace ExchangeRateUpdater.Application.Contracts;

public interface ICzechBankExchangeRateService
{
    Task<IEnumerable<CzechBankExchangeRateDto>> GetExchangeRatesAsync(DateTime date, Language language);
}
