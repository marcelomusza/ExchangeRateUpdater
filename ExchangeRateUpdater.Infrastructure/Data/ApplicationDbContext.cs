using ExchangeRateUpdater.Domain.Model;
using ExchangeRateUpdater.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace ExchangeRateUpdater.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<ExchangeRate> ExchangeRates { get; set; }
    public DbSet<Bank> Banks { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {        
        modelBuilder.ApplyConfiguration(new CurrencyEntityConfiguration());
        modelBuilder.ApplyConfiguration(new BankEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ExchangeRateEntityConfiguration());
    }
}
