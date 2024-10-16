using ExchangeRateUpdater.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExchangeRateUpdater.Infrastructure.EntityConfigurations;

public class ExchangeRateEntityConfiguration : IEntityTypeConfiguration<ExchangeRate>
{
    public void Configure(EntityTypeBuilder<ExchangeRate> builder)
    {
        builder.HasKey(x => x.Id);  

        builder.Property(x => x.Date)
            .IsRequired();

        builder.Property(x => x.Value)
            .IsRequired()
            .HasPrecision(18, 4);

        builder.HasOne(x => x.SourceCurrency)
            .WithMany()
            .HasForeignKey(x => x.SourceCurrencyId)
            .IsRequired();

        builder.Property(er => er.SourceCurrencyId)
            .HasColumnName("SourceCurrency");

        builder.HasOne(x => x.TargetCurrency)
            .WithMany()
            .HasForeignKey(x => x.TargetCurrencyId)
            .IsRequired();

        builder.Property(er => er.TargetCurrencyId)
            .HasColumnName("TargetCurrency");

        builder.HasOne(x => x.Bank)
            .WithMany()
            .HasForeignKey(x => x.BankId)
            .IsRequired();

        builder.Property(er => er.BankId)
            .HasColumnName("Bank");
    }
}
