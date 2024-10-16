using ExchangeRateUpdater.Application.Contracts.Services;
using MediatR;

namespace ExchangeRateUpdater.Application.Commands;

public class CzechBankExchangeRatesCommandHandler : IRequestHandler<CzechBankExchangeRatesCommand, bool>
{
    private readonly ICzechBankExchangeRateProcessorService _processorService;

    public CzechBankExchangeRatesCommandHandler(ICzechBankExchangeRateProcessorService processorService)
    {
        _processorService = processorService;
    }

    public async Task<bool> Handle(CzechBankExchangeRatesCommand request, CancellationToken cancellationToken)
    {
        return await _processorService.ProcessExchangeRatesAsync(request.Date, request.Language);
    }
}
