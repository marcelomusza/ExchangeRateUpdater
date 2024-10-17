namespace ExchangeRateUpdater.Domain.Model;

public class ExchangeRate
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public int SourceCurrencyId { get;  set; }
    public Currency SourceCurrency { get; set; }

    public int TargetCurrencyId { get; set; }
    public Currency TargetCurrency { get; set; }

    public decimal Value { get; set; }

    public int BankId { get; set; }
    public Bank Bank { get; set; }

    protected ExchangeRate() { }

    public ExchangeRate(DateTime date,
                        Currency sourceCurrency,
                        Currency targetCurrency,
                        decimal value,
                        Bank bank)
    {
        Date = date;
        SourceCurrency = sourceCurrency;
        TargetCurrency = targetCurrency;
        Value = value;
        Bank = bank;
    }

    public override string ToString() => $"{Date.ToShortDateString()}: {SourceCurrency}/{TargetCurrency} = {Value}";
}
