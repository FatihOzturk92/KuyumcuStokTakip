using KuyumcuStokTakip.Domain.Entities.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KuyumcuStokTakip.Infrastructure.Data.Configurations;

public class ProductItemConfiguration : IEntityTypeConfiguration<ProductItem>
{
    public void Configure(EntityTypeBuilder<ProductItem> builder)
    {
        builder.ToTable("ProductItems");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Barcode)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(i => i.Size)
            .HasMaxLength(100);

        builder.Property(i => i.Description)
            .HasMaxLength(500);

        builder.Property(i => i.Weight).HasColumnType("decimal(18,2)");
        builder.Property(i => i.LaborCost).HasColumnType("decimal(18,2)");
        builder.Property(i => i.Cost).HasColumnType("decimal(18,2)");

        builder.HasOne(i => i.InventoryProduct)
            .WithMany()
            .HasForeignKey(i => i.InventoryProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(i => i.StockTransaction)
            .WithMany()
            .HasForeignKey(i => i.StockTransactionId);

        builder.HasOne(i => i.Purity)
            .WithMany()
            .HasForeignKey(i => i.PurityId);
    }
}
