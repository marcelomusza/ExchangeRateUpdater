using ExchangeRateUpdater.Application.Commands;

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
    }
}
