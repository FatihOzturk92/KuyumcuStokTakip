using KuyumcuStokTakip.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KuyumcuStokTakip.Infrastructure.Data.Configurations;

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.ToTable("Branches");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(b => b.Location)
            .HasMaxLength(200);

        builder.Property(b => b.Phone)
            .HasMaxLength(50);

        builder.Property(b => b.Email)
            .HasMaxLength(100);

        builder.Ignore(b => b.InventoryProducts);
        builder.Ignore(b => b.CashTransactions);
    }
}
