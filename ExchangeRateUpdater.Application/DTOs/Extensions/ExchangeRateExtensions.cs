using ExchangeRateUpdater.Application.Commands;
using ExchangeRateUpdater.Domain.Model;

namespace ExchangeRateUpdater.Application.DTOs.Extensions
{
    public static class ExchangeRateExtensions
    {
        public static CzechBankExchangeRatesCommand Map(this CzechBankRequestDto value)
        {
            if (value == null)
                return null;

            return new CzechBankExchangeRatesCommand(value.Date,
                                                      value.Language);
        }

        public static ExchangeRate MapToDB(this CzechBankExchangeRateDto value, 
                                            Bank bankName, 
                                            Currency sourceCurrency,
                                            IEnumerable<Currency> currencies)
        {
            if (value == null)
                return null;

            var targetCurrency = currencies.First(x => x.Code == value.CurrencyCode);

            return new ExchangeRate(value.ValidFor,
                                    sourceCurrency,
                                    targetCurrency,
                                    Math.Round(value.Rate, 4),
                                    bankName);
                                                      
        }

        public static IEnumerable<ExchangeRate> MapToDBCollection(this IEnumerable<CzechBankExchangeRateDto> valueCollection,
                                                                  Bank bankName,
                                                                  Currency sourceCurrency,
                                                                  IEnumerable<Currency> currencies)
        {
            if (valueCollection == null)
                return Enumerable.Empty<ExchangeRate>();

            return valueCollection
                .Select(dto => dto.MapToDB(bankName, sourceCurrency, currencies))
                .Where(exchangeRate => exchangeRate != null)
                .ToList();
        }

        public static ExchangeRateDto MapToDto(this ExchangeRate exchangeRate)
        {
            if (exchangeRate == null)
                return null;

            return new ExchangeRateDto
            {
                Date = exchangeRate.Date,
                SourceCurrency = exchangeRate.SourceCurrency?.Code!,
                TargetCurrency = exchangeRate.TargetCurrency?.Code!,
                Value = exchangeRate.Value,
                BankName = exchangeRate.Bank?.Name!
            };
        }

        public static IEnumerable<ExchangeRateDto> MapToDtoCollection(this IEnumerable<ExchangeRate> exchangeRates)
        {
            if (exchangeRates == null)
                return Enumerable.Empty<ExchangeRateDto>();

            return exchangeRates.Select(exchangeRate => exchangeRate.MapToDto()).ToList();
        }
    }
}
