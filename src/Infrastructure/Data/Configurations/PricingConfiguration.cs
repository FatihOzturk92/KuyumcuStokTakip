using KuyumcuStokTakip.Domain.Entities.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KuyumcuStokTakip.Infrastructure.Data.Configurations;

public class PricingConfiguration : IEntityTypeConfiguration<Pricing>
{
    public void Configure(EntityTypeBuilder<Pricing> builder)
    {
        builder.ToTable("Pricings");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.PurchasePrice).HasColumnType("decimal(18,2)");
        builder.Property(p => p.SalePrice).HasColumnType("decimal(18,2)");

        builder.HasOne<ProductItem>()
            .WithMany()
            .HasForeignKey(p => p.ProductItemId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
