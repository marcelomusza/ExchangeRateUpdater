using ExchangeRateUpdater.Application.Contracts;
using ExchangeRateUpdater.Application.DTOs.Extensions;
using ExchangeRateUpdater.Domain.Interfaces;
using ExchangeRateUpdater.Domain.Model;
using MediatR;

namespace ExchangeRateUpdater.Application.Commands;

public class CzechBankExchangeRatesCommandHandler : IRequestHandler<CzechBankExchangeRatesCommand, bool>
{
    private readonly ICzechBankExchangeRateService _exchangeRateService;
    private readonly IExchangeRateRepository _exchangeRateRepository;

    private const string BankName = "CzechBank";
    private const string SourceCurrency = "CZK";

    public CzechBankExchangeRatesCommandHandler(ICzechBankExchangeRateService exchangeRateService,
        IExchangeRateRepository exchangeRateRepository)
    {
        _exchangeRateService = exchangeRateService;
        _exchangeRateRepository = exchangeRateRepository;
    }

    public async Task<bool> Handle(CzechBankExchangeRatesCommand request, CancellationToken cancellationToken)
    {

        var exchangeRates = await _exchangeRateService.GetExchangeRatesAsync(request.Date, request.Language);

        var rates = exchangeRates.MapToDBCollection(BankName, SourceCurrency);

        var result = await _exchangeRateRepository.AddExchangeRatesAsync(rates);
        

        return result;
    }
}
