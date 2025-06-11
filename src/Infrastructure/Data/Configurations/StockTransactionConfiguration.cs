using KuyumcuStokTakip.Domain.Entities.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KuyumcuStokTakip.Infrastructure.Data.Configurations;

public class StockTransactionConfiguration : IEntityTypeConfiguration<StockTransaction>
{
    public void Configure(EntityTypeBuilder<StockTransaction> builder)
    {
        builder.ToTable("StockTransactions");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.PureGram).HasColumnType("decimal(18,2)");
        builder.Property(t => t.PureUnitPrice).HasColumnType("decimal(18,2)");
        builder.Property(t => t.LaborUnitPrice).HasColumnType("decimal(18,2)");
        builder.Property(t => t.Quantity).HasColumnType("decimal(18,2)");
        builder.Property(t => t.UnitPrice).HasColumnType("decimal(18,2)");
        builder.Property(t => t.Weight).HasColumnType("decimal(18,2)");
        builder.Property(t => t.TotalCost).HasColumnType("decimal(18,2)");
        builder.Property(t => t.ProductId);
        builder.Property(t => t.TransactionType).HasConversion<int>();

        builder.Property(t => t.Description).HasMaxLength(500);

        builder.HasOne(t => t.InventoryProduct)
            .WithMany(p => p.Transactions)
            .HasForeignKey(t => t.InventoryProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(t => t.ProductItem)
            .WithMany()
            .HasForeignKey(t => t.ProductItemId);

        builder.HasOne(t => t.SourceCompany)
            .WithMany()
            .HasForeignKey(t => t.SourceCompanyId);

        builder.HasOne(t => t.TargetCompany)
            .WithMany()
            .HasForeignKey(t => t.TargetCompanyId);

        builder.HasOne(t => t.Customer)
            .WithMany()
            .HasForeignKey(t => t.CustomerId);
    }
}
