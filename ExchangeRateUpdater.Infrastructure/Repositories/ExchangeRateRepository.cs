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
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding exchange rates: {ex.Message}");
            return false;
        }
    }

    public async Task<IEnumerable<ExchangeRate>> GetExchangeRatesByDayAsync(int bankId, DateTime date)
    {
        return await _context.ExchangeRates
            .Include(x => x.Bank)
            .Include(x => x.SourceCurrency)
            .Include(x => x.TargetCurrency)
            .Where(x => x.Date == date && x.BankId == bankId)
            .ToListAsync();
    }

    public async Task<IEnumerable<ExchangeRate>> GetAllExchangeRatesAsync()
    {
        return await _context.ExchangeRates
            .ToListAsync();
    }

    public async Task<Bank> GetBankAsync(int bankId)
    {
        var bank = await _context.Banks.FirstOrDefaultAsync(x => x.Id == bankId);

        if (bank == null)
        {
            return null;
        }       

        return bank;
    }

    public async Task<IEnumerable<Currency>> GetCurrenciesListAsync()
    {
        return await _context.Currencies
            .ToListAsync();
    }

    public async Task<Currency> GetSourceCurrencyAsync(string sourceCurrency)
    {
        return await _context.Currencies
            .FirstOrDefaultAsync(x => x.Code == sourceCurrency);
    }

    public async Task<bool> HasRatesForDateAsync(int bankId, DateTime date)
    {
        return await _context.ExchangeRates.AnyAsync(x => x.Date == date 
                                                       && x.BankId == bankId);
    }

    public async Task<IEnumerable<ExchangeRate>> GetExchangeRatesByBankAsync(int bankId)
    {
        return await _context.ExchangeRates
            .Include(x => x.Bank)
            .Include(x => x.SourceCurrency)
            .Include(x => x.TargetCurrency)
            .Where(x => x.BankId == bankId)
            .ToListAsync();
    }
}
