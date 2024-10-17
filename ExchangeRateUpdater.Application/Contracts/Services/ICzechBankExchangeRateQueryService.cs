using ExchangeRateUpdater.Application.DTOs;

namespace ExchangeRateUpdater.Application.Contracts.Services;

public interface ICzechBankExchangeRateQueryService
{
    Task<IEnumerable<ExchangeRateDto>> GetExchangeRateByDayAsync(int bankId, DateTime date);
    Task<ExchangeRateResponseDto> GetExchangeRatesAsync(int bankId, IEnumerable<CurrencyDto> currencyCodes);
}
