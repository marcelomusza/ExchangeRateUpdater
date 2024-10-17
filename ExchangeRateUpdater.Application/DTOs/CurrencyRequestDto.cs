namespace ExchangeRateUpdater.Application.DTOs;

public class CurrencyRequestDto
{
    public IEnumerable<CurrencyDto> Currencies { get; set; }
}
