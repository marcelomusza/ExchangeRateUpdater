using ExchangeRateUpdater.Application.DTOs;
using MediatR;

namespace ExchangeRateUpdater.Application.Queries;

public record CzechBankGetExchangeRateByDayQuery(int BankId, DateTime Date) : IRequest<IEnumerable<ExchangeRateDto>>;
