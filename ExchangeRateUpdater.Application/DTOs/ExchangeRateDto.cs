namespace ExchangeRateUpdater.Application.DTOs;

public class ExchangeRateDto
{
    public DateTime Date { get; set; }
    public string SourceCurrency { get; set; }
    public string TargetCurrency { get; set; }
    public decimal Value { get; set; }
    public string BankName { get; set; }
}
