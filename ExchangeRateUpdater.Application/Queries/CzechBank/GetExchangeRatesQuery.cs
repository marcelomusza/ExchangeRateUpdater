using ExchangeRateUpdater.Application.DTOs;
using MediatR;

namespace ExchangeRateUpdater.Application.Queries.CzechBank;

public record GetExchangeRatesQuery(int BankId, IEnumerable<CurrencyDto> CurrencyCodes) : IRequest<ExchangeRateResponseDto>;