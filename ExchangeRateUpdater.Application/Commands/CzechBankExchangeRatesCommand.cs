using ExchangeRateUpdater.Application.DTOs.Enums;
using MediatR;

namespace ExchangeRateUpdater.Application.Commands;

public class CzechBankExchangeRatesCommand : IRequest<bool>
{
    public DateTime Date { get; set; }
    public Language Language { get; set; }

    public CzechBankExchangeRatesCommand(DateTime date, Language language)
    {
        Date = date;
        Language = language;
    }
}
