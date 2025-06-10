using KuyumcuStokTakip.Domain.Entities.Finance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KuyumcuStokTakip.Infrastructure.Data.Configurations;

public class PartnerAccountTransactionConfiguration : IEntityTypeConfiguration<PartnerAccountTransaction>
{
    public void Configure(EntityTypeBuilder<PartnerAccountTransaction> builder)
    {
        builder.ToTable("PartnerAccountTransactions");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Amount).HasColumnType("decimal(18,2)");
        builder.Property(p => p.Description).HasMaxLength(500);

        builder.HasOne(p => p.Partner)
            .WithMany()
            .HasForeignKey(p => p.PartnerId);
    }
}
