using ExchangeRateUpdater.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExchangeRateUpdater.Infrastructure.EntityConfigurations;

public class BankEntityConfiguration : IEntityTypeConfiguration<Bank>
{
    public void Configure(EntityTypeBuilder<Bank> builder)
    {
        builder.HasKey(b => b.Id);  

        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
