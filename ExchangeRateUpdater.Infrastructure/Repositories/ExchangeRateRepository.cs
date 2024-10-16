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

    public async Task<ExchangeRate> GetExchangeRatesByDayAsync(DateTime date)
    {
        return await _context.ExchangeRates.
            FirstOrDefaultAsync(x => x.Date == date);
    }

    public async Task<IEnumerable<ExchangeRate>> GetAllExchangeRatesAsync()
    {
        return await _context.ExchangeRates
            .ToListAsync();
    }

    public async Task<Bank> GetOrCreateBankAsync(string bankName)
    {
        var existingBank = await _context.Banks.FirstOrDefaultAsync(x => x.Name == bankName);

        if (existingBank != null)
        {
            return existingBank;
        }

        var newBank = new Bank(bankName);

        _context.Banks.Add(newBank);

        return newBank;
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

    public async Task<bool> HasRatesForDateAsync(DateTime date)
    {
        return await _context.ExchangeRates.AnyAsync(x => x.Date == date);
    }
}
