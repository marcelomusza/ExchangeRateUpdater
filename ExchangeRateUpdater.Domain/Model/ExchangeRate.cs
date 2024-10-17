namespace ExchangeRateUpdater.Domain.Model;

public class ExchangeRate
{
    public int Id { get; private set; }

    public DateTime Date { get; private set; }

    public int SourceCurrencyId { get; private set; }
    public Currency SourceCurrency { get; private set; }

    public int TargetCurrencyId { get; private set; }
    public Currency TargetCurrency { get; private set; }

    public decimal Value { get; private set; }

    public int BankId { get; private set; }
    public Bank Bank { get; private set; }

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
