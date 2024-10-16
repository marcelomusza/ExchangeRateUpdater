using ExchangeRateUpdater.Domain.Interfaces;
using ExchangeRateUpdater.Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace ExchangeRateUpdater.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<UnitOfWork> _logger;

    public IExchangeRateRepository ExchangeRateRepository { get; }

    public UnitOfWork(ApplicationDbContext context, IExchangeRateRepository exchangeRateRepository, ILogger<UnitOfWork> logger)
    {
        _context = context;
        ExchangeRateRepository = exchangeRateRepository;
        _logger = logger;
    }

    public async Task<bool> SaveChangesAsync()
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Transaction failed.");
            return false;
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
