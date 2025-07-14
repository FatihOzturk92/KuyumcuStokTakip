using KuyumcuStokTakip.Domain.Entities.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KuyumcuStokTakip.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.ModelName)
            .HasMaxLength(200);

        builder.Property(p => p.PhotoUrl)
            .HasMaxLength(300);

        builder.Property(p => p.CertificateNumber)
            .HasMaxLength(100);

        builder.Property(p => p.Notes)
            .HasMaxLength(1000);
    }
}
