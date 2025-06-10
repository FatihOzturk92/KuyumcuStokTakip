using KuyumcuStokTakip.Domain.Entities.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KuyumcuStokTakip.Infrastructure.Data.Configurations;

public class StockUnitConfiguration : IEntityTypeConfiguration<StockUnit>
{
    public void Configure(EntityTypeBuilder<StockUnit> builder)
    {
        builder.ToTable("StockUnits");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.Symbol)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(u => u.Description)
            .HasMaxLength(500);

        builder.HasMany(u => u.InventoryProducts)
            .WithOne(p => p.Unit)
            .HasForeignKey(p => p.UnitId);
    }
}
