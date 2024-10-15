using ExchangeRateUpdater.Application.Contracts;
using MediatR;

namespace ExchangeRateUpdater.Application.Commands;

public class CzechBankExchangeRatesCommandHandler : IRequestHandler<CzechBankExchangeRatesCommand, bool>
{
    private readonly ICzechBankExchangeRateService _exchangeRateService;

    public CzechBankExchangeRatesCommandHandler(ICzechBankExchangeRateService exchangeRateService)
    {
        _exchangeRateService = exchangeRateService;
    }

    public async Task<bool> Handle(CzechBankExchangeRatesCommand request, CancellationToken cancellationToken)
    {

        var exchangeRates = await _exchangeRateService.GetExchangeRatesAsync(request.Date, request.Language);

        // TODO: Add processing logic here if needed

        // In the future, you would save the data to the database here
        // For now, simply return true to indicate success
        return true;
    }
}
