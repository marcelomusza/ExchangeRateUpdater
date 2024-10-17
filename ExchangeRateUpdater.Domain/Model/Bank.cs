namespace ExchangeRateUpdater.Domain.Model;

public class Bank
{
    public int Id { get; set; }

    public string Name { get; private set; }

    public Bank(string name)
    {
        Name = name;
    }
}
