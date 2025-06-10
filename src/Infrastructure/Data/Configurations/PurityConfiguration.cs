using KuyumcuStokTakip.Domain.Entities.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KuyumcuStokTakip.Infrastructure.Data.Configurations;

public class PurityConfiguration : IEntityTypeConfiguration<Purity>
{
    public void Configure(EntityTypeBuilder<Purity> builder)
    {
        builder.ToTable("Purities");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasMaxLength(50)
            .IsRequired();
    }
}
