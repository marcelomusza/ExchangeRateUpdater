using ExchangeRateUpdater.Domain.Interfaces;
using ExchangeRateUpdater.Domain.Model;
using ExchangeRateUpdater.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExchangeRateUpdater.Infrastructure.Repositories;

public class ExchangeRateRepository : IExchangeRateRepository
{
    private readonly ApplicationDbContext _context;

    public ExchangeRateRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AddExchangeRatesAsync(IEnumerable<ExchangeRate> exchangeRates)
    {        
        try
        {
            await _context.ExchangeRates.AddRangeAsync(exchangeRates);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding exchange rates: {ex.Message}");
            return false;
        }
    }

    public async Task<ExchangeRate> GetExchangeRatesByDayAsync(DateTime date)
    {
        return await _context.ExchangeRates.
            FirstOrDefaultAsync(s => s.Date == date);
    }

    public async Task<IEnumerable<ExchangeRate>> GetAllExchangeRatesAsync()
    {
        return await _context.ExchangeRates
            .ToListAsync();
    }

}
