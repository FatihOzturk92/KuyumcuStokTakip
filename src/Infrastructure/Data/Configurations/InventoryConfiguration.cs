using KuyumcuStokTakip.Domain.Entities.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KuyumcuStokTakip.Infrastructure.Data.Configurations;

public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.ToTable("Inventories");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Code)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(i => i.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(i => i.Description)
            .HasMaxLength(500);

        builder.HasOne(i => i.AccountPartner)
            .WithMany()
            .HasForeignKey(i => i.AccountPartnerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Ignore(i => i.Transactions);
    }
}
