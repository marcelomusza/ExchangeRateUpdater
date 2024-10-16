using AutoMapper;
using ExchangeRateUpdater.Application.DTOs;
using ExchangeRateUpdater.Application.DTOs.Extensions;
using ExchangeRateUpdater.Domain.Interfaces;
using MediatR;

namespace ExchangeRateUpdater.Application.Queries.CzechBank;

public class GetExchangeRateQueryHandler : IRequestHandler<GetExchangeRateByDayQuery, IEnumerable<ExchangeRateDto>>
{
    private readonly IExchangeRateRepository _exchangeRateRepository;
    private readonly IMapper _mapper;

    public GetExchangeRateQueryHandler(IExchangeRateRepository exchangeRateRepository,
                                       IMapper mapper)
    {
        _exchangeRateRepository = exchangeRateRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<ExchangeRateDto>> Handle(GetExchangeRateByDayQuery request, CancellationToken cancellationToken)
    {
        var exchangeRates = await _exchangeRateRepository.GetExchangeRatesByDayAsync(request.Date);
        return exchangeRates.MapToDtoCollection();
    }
}