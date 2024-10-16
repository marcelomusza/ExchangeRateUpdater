namespace ExchangeRateUpdater.Domain.Model;

public class GlobalExchangeRate
{

    public DateTime Date  { get; set; }

    public string SourceCurrency { get; set; }

    public string TargetCurrency { get; set; }

    public decimal Value { get; set;  }

}
