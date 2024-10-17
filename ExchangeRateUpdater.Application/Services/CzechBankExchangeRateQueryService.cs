using ExchangeRateUpdater.Application.Contracts.External;
using ExchangeRateUpdater.Application.Contracts.Services;
using ExchangeRateUpdater.Application.DTOs;
using ExchangeRateUpdater.Application.DTOs.Extensions;
using ExchangeRateUpdater.Domain.Interfaces;

namespace ExchangeRateUpdater.Application.Services;

public class CzechBankExchangeRateQueryService : ICzechBankExchangeRateQueryService
{
    private readonly ICzechBankExchangeRateService _exchangeRateService;
    private readonly IExchangeRateRepository _exchangeRateRepository;
    private readonly IUnitOfWork _unitOfWork;

    private const string BankName = "Czech National Bank";
    private const string SourceCurrency = "CZK";

    public CzechBankExchangeRateQueryService(ICzechBankExchangeRateService exchangeRateService,
                                                 IExchangeRateRepository exchangeRateRepository,
                                                 IUnitOfWork unitOfWork)
    {
        _exchangeRateService = exchangeRateService;
        _exchangeRateRepository = exchangeRateRepository;
        _unitOfWork = unitOfWork;
    }

    

    public async Task<IEnumerable<ExchangeRateDto>> GetExchangeRateByDayAsync(int bankId, DateTime date)
    {
        var exchangeRates = await _exchangeRateRepository.GetExchangeRatesByDayAsync(bankId, date);
        return exchangeRates.MapToDtoCollection();
    }

    public async Task<ExchangeRateResponseDto> GetExchangeRatesAsync(int bankId, IEnumerable<CurrencyDto> currencyCodes)
    {
        var allExchangeRates = await _exchangeRateRepository.GetExchangeRatesByBankAsync(bankId);

        var filteredRates = allExchangeRates
                                .Where(rate => rate.SourceCurrency.Code.Equals(SourceCurrency, StringComparison.OrdinalIgnoreCase)
                                            && currencyCodes.Any(c => c.Code.Equals(rate.TargetCurrency.Code, StringComparison.OrdinalIgnoreCase))) 
                                .ToList();

        // Map to the response DTO
        var formattedRates = filteredRates.Select(rate => rate.ToString()).ToList();
        var bankName = filteredRates.FirstOrDefault()!.Bank.Name;

        return new ExchangeRateResponseDto
        {
            BankName = bankName,
            ExchangeRates = formattedRates
        };

    }
}
