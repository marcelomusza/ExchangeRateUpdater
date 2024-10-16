namespace ExchangeRateUpdater.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IExchangeRateRepository ExchangeRateRepository { get; }
    Task<bool> SaveChangesAsync();
}
