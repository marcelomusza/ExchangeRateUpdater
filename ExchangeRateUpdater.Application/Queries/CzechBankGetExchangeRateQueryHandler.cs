using AutoMapper;
using ExchangeRateUpdater.Application.Contracts.Services;
using ExchangeRateUpdater.Application.DTOs;
using ExchangeRateUpdater.Application.DTOs.Extensions;
using ExchangeRateUpdater.Domain.Interfaces;
using MediatR;

namespace ExchangeRateUpdater.Application.Queries;

public class CzechBankGetExchangeRateQueryHandler : IRequestHandler<CzechBankGetExchangeRateByDayQuery, IEnumerable<ExchangeRateDto>>,
                                           IRequestHandler<CzechBankGetExchangeRatesQuery, ExchangeRateResponseDto>
{
    private readonly ICzechBankExchangeRateQueryService _processorService;
    private readonly IExchangeRateRepository _exchangeRateRepository;
    private readonly IMapper _mapper;

    public CzechBankGetExchangeRateQueryHandler(ICzechBankExchangeRateQueryService processorService,
                                       IExchangeRateRepository exchangeRateRepository,
                                       IMapper mapper)
    {
        _exchangeRateRepository = exchangeRateRepository;
        _mapper = mapper;
        _processorService = processorService;
    }
    public async Task<IEnumerable<ExchangeRateDto>> Handle(CzechBankGetExchangeRateByDayQuery request, CancellationToken cancellationToken)
    {
        return await _processorService.GetExchangeRateByDayAsync(request.BankId, request.Date);
    }

    public async Task<ExchangeRateResponseDto> Handle(CzechBankGetExchangeRatesQuery request, CancellationToken cancellationToken)
    {
        return await _processorService.GetExchangeRatesAsync(request.BankId, request.CurrencyCodes);
    }
}