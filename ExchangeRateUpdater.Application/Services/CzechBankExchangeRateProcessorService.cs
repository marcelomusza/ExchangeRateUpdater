﻿using ExchangeRateUpdater.Application.Contracts.External;
using ExchangeRateUpdater.Application.Contracts.Services;
using ExchangeRateUpdater.Application.DTOs;
using ExchangeRateUpdater.Application.DTOs.Enums;
using ExchangeRateUpdater.Application.DTOs.Extensions;
using ExchangeRateUpdater.Domain.Interfaces;
using MediatR;

namespace ExchangeRateUpdater.Application.Services;



public class CzechBankExchangeRateProcessorService : ICzechBankExchangeRateProcessorService
{
    private readonly ICzechBankExchangeRateService _exchangeRateService;
    private readonly IExchangeRateRepository _exchangeRateRepository;
    private readonly IUnitOfWork _unitOfWork;

    private const string BankName = "Czech National Bank";
    private const string SourceCurrency = "CZK";

    public CzechBankExchangeRateProcessorService(ICzechBankExchangeRateService exchangeRateService,
                                                 IExchangeRateRepository exchangeRateRepository,
                                                 IUnitOfWork unitOfWork)
    {
        _exchangeRateService = exchangeRateService;
        _exchangeRateRepository = exchangeRateRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> ProcessExchangeRatesAsync(int bankId, DateTime date, Language language)
    {
        // Prevent duplicate processing for the same date
        if (await _exchangeRateRepository.HasRatesForDateAsync(bankId, date))
        {
            return false;
        }

        var bank = await _unitOfWork.ExchangeRateRepository.GetBankAsync(bankId);
        var exchangeRates = await _exchangeRateService.GetExchangeRatesAsync(date, language);
        var currencies = await _unitOfWork.ExchangeRateRepository.GetCurrenciesListAsync();
        var sourceCurrency = await _unitOfWork.ExchangeRateRepository.GetSourceCurrencyAsync(SourceCurrency);
        

        var rates = exchangeRates.MapToDBCollection(bank, sourceCurrency, currencies);

        await _unitOfWork.ExchangeRateRepository.AddExchangeRatesAsync(rates);

        return await _unitOfWork.SaveChangesAsync();
    }

}
