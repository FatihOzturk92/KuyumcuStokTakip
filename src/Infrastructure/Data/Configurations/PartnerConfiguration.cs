using KuyumcuStokTakip.Domain.Entities.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KuyumcuStokTakip.Infrastructure.Data.Configurations;

public class PartnerConfiguration : IEntityTypeConfiguration<Partner>
{
    public void Configure(EntityTypeBuilder<Partner> builder)
    {
        builder.ToTable("Partners");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.Type)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.ParnerPhone)
            .HasMaxLength(50);

        builder.Property(p => p.ParnerEmail)
            .HasMaxLength(100);

        builder.Property(p => p.ParnerAddress)
            .HasMaxLength(500);

        builder.Property(p => p.Note)
            .HasMaxLength(500);

        builder.Ignore(p => p.IncomingTransactions);
        builder.Ignore(p => p.OutgoingTransactions);
        builder.Ignore(p => p.AccountTransactions);
    }
}
