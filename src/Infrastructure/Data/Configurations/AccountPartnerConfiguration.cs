using KuyumcuStokTakip.Domain.Entities.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KuyumcuStokTakip.Infrastructure.Data.Configurations;

public class AccountPartnerConfiguration : IEntityTypeConfiguration<AccountPartner>
{
    public void Configure(EntityTypeBuilder<AccountPartner> builder)
    {
        builder.ToTable("AccountPartners");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(a => a.LastName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(a => a.Phone)
            .HasMaxLength(50);

        builder.Property(a => a.Email)
            .HasMaxLength(100);

        builder.Property(a => a.Address)
            .HasMaxLength(500);

        builder.Property(a => a.Notes)
            .HasMaxLength(500);

        builder.HasOne(a => a.Partner)
            .WithMany(p => p.ContactPersons)
            .HasForeignKey(a => a.PartnerId);
    }
}
