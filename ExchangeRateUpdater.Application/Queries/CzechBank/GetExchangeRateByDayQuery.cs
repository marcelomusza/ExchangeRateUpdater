using ExchangeRateUpdater.Application.DTOs;
using MediatR;

namespace ExchangeRateUpdater.Application.Queries.CzechBank;

public record GetExchangeRateByDayQuery(DateTime Date) : IRequest<IEnumerable<ExchangeRateDto>>;
