using ExchangeRateUpdater.Domain.Model;

namespace ExchangeRateUpdater.Infrastructure.EntityConfigurations.Seed;

public static class BankSeed
{
    public static List<Bank> GetBanks()
    {
        var banksList = new List<Bank>
        {
            new Bank("Czech National Bank") { Id = 1 }
        };
            
        return banksList;
    }
}
