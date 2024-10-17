using ExchangeRateUpdater.Application.DTOs;
using MediatR;

namespace ExchangeRateUpdater.Application.Queries;

public record CzechBankGetExchangeRatesQuery(int BankId, IEnumerable<CurrencyDto> CurrencyCodes) : IRequest<ExchangeRateResponseDto>;