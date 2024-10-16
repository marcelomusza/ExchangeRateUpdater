using ExchangeRateUpdater.Domain.Model;
using ExchangeRateUpdater.Infrastructure.EntityConfigurations.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExchangeRateUpdater.Infrastructure.EntityConfigurations;

public class CurrencyEntityConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.HasKey(c => c.Id); 

        builder.Property(c => c.Code)
            .IsRequired()
            .HasMaxLength(3);

        builder.HasData(CurrencySeed.GetCurrencies());
    }
}
