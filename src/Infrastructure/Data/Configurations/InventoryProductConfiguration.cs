using KuyumcuStokTakip.Domain.Entities.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KuyumcuStokTakip.Infrastructure.Data.Configurations;

public class InventoryProductConfiguration : IEntityTypeConfiguration<InventoryProduct>
{
    public void Configure(EntityTypeBuilder<InventoryProduct> builder)
    {
        builder.ToTable("InventoryProducts");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Code)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(500);

        builder.Property(p => p.TotalWeight).HasColumnType("decimal(18,2)");
        builder.Property(p => p.TotalPieceCount).HasColumnType("decimal(18,2)");
        builder.Property(p => p.TotalLaborCost).HasColumnType("decimal(18,2)");
        builder.Property(p => p.TotalMaterialCost).HasColumnType("decimal(18,2)");
        builder.Ignore(p => p.TotalCost);

        builder.HasOne(p => p.Category)
            .WithMany(c => c.InventoryProducts)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Inventory)
            .WithMany()
            .HasForeignKey(p => p.InventoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Unit)
            .WithMany(u => u.InventoryProducts)
            .HasForeignKey(p => p.UnitId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Transactions)
            .WithOne(t => t.InventoryProduct)
            .HasForeignKey(t => t.InventoryProductId);
    }
}
