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
                                    value.Rate,
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
    }
}
