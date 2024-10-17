namespace ExchangeRateUpdater.Application.DTOs;

public class ExchangeRateResponseDto
{
    public string BankName { get; set; }
    public IEnumerable<string> ExchangeRates { get; set; }
}
