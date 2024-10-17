namespace ExchangeRateUpdater.Domain.Model;

public class Bank
{
    public int Id { get; set; }

    public string Name { get; set; }

    public Bank(string name)
    {
        Name = name;
    }
}
