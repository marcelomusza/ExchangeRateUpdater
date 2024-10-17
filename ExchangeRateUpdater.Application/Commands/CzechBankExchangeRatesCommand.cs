using ExchangeRateUpdater.Application.DTOs.Enums;
using MediatR;

namespace ExchangeRateUpdater.Application.Commands;

public class CzechBankExchangeRatesCommand : IRequest<bool>
{
    public int BankId { get; set; }
    public DateTime Date { get; set; }
    public Language Language { get; set; }

    public CzechBankExchangeRatesCommand(int bankId, DateTime date, Language language)
    {
        BankId = bankId;
        Date = date;
        Language = language;
    }
}
