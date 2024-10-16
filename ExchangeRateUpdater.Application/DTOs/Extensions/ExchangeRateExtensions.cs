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

        public static ExchangeRate MapToDB(this CzechBankExchangeRateDto value, string bankName, string sourceCurrency)
        {
            if (value == null)
                return null;

            return new ExchangeRate(value.ValidFor,
                                    new Currency(sourceCurrency),
                                    new Currency(value.CurrencyCode),
                                    value.Rate,
                                    new Bank(bankName));
                                                      
        }

        public static IEnumerable<ExchangeRate> MapToDBCollection(this IEnumerable<CzechBankExchangeRateDto> valueCollection,
                                                                  string bankName,
                                                                  string sourceCurrency)
        {
            if (valueCollection == null)
                return Enumerable.Empty<ExchangeRate>();

            return valueCollection
                .Select(dto => dto.MapToDB(bankName, sourceCurrency))
                .Where(exchangeRate => exchangeRate != null)
                .ToList();
        }
    }
}
