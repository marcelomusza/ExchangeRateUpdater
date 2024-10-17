namespace ExchangeRateUpdater.Domain.Model;

public class Currency
{
    public int Id { get; set; }

    public Currency()
    {

    }
    
    public Currency(string code)
    {
        Code = code;
    }

    public string Code { get; set; }

    public override string ToString() => Code;
}
