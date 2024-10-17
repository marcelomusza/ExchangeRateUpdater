using ExchangeRateUpdater.Application.DTOs.Enums;

namespace ExchangeRateUpdater.Application.DTOs;

public class CzechBankRequestDto
{
    public int BankId { get; set; }
    public DateTime Date { get; set; }
    public Language Language { get; set; }
}
