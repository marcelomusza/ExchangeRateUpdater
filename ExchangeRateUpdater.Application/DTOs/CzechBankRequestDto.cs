using ExchangeRateUpdater.Application.DTOs.Enums;

namespace ExchangeRateUpdater.Application.DTOs;

public class CzechBankRequestDto
{
    public DateTime Date { get; set; }
    public Language Language { get; set; }
}
