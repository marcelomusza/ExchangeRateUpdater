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
    private readonly IUnitOfWork _unitOfWork;

    private const string BankName = "Czech National Bank";
    private const string SourceCurrency = "CZK";

    public CzechBankExchangeRatesCommandHandler(ICzechBankExchangeRateService exchangeRateService,
        IExchangeRateRepository exchangeRateRepository,
        IUnitOfWork unitOfWork)
    {
        _exchangeRateService = exchangeRateService;
        _exchangeRateRepository = exchangeRateRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(CzechBankExchangeRatesCommand request, CancellationToken cancellationToken)
    {
        //Daily operation, should not allow multiple insertions for the same date
        if (await _exchangeRateRepository.HasRatesForDateAsync(request.Date))
        {
            return false; 
        }

        var exchangeRates = await _exchangeRateService.GetExchangeRatesAsync(request.Date, request.Language);

        var currencies = await _unitOfWork.ExchangeRateRepository.GetCurrenciesListAsync();
        var sourceCurrency = await _unitOfWork.ExchangeRateRepository.GetSourceCurrencyAsync(SourceCurrency);
        var bank = await _unitOfWork.ExchangeRateRepository.GetOrCreateBankAsync(BankName);

        var rates = exchangeRates.MapToDBCollection(bank, sourceCurrency, currencies);

        await _unitOfWork.ExchangeRateRepository.AddExchangeRatesAsync(rates);

        return await _unitOfWork.SaveChangesAsync();
    }
}
