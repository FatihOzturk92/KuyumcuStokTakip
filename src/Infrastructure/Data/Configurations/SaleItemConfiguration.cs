using KuyumcuStokTakip.Domain.Entities.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KuyumcuStokTakip.Infrastructure.Data.Configurations;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("SaleItems");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Quantity).HasColumnType("decimal(18,2)");
        builder.Property(i => i.UnitPrice).HasColumnType("decimal(18,2)");

        builder.Ignore(i => i.Total);

        builder.HasOne(i => i.Sale)
            .WithMany(s => s.Items)
            .HasForeignKey(i => i.SaleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(i => i.ProductItem)
            .WithMany()
            .HasForeignKey(i => i.ProductItemId);

        builder.HasOne(i => i.InventoryProduct)
            .WithMany()
            .HasForeignKey(i => i.InventoryProductId);
    }
}
