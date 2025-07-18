using System.ComponentModel.DataAnnotations.Schema;
using KuyumcuStokTakip.Domain.Entities.Account;

namespace KuyumcuStokTakip.Domain.Entities.Sales;

public class Sale : BaseAuditableEntity
{
    public DateTime SaleDate { get; set; } = DateTime.UtcNow;
    public int? CustomerId { get; set; }
    public Customer? Customer { get; set; }

    [NotMapped]
    public decimal TotalAmount => Items.Sum(i => i.Total);
    public EPaymentType PaymentMethod { get; set; } = default!; // Paypal, Kredi Kartı, Nakit, Banka Havale, EFT vb.
    public string? Currency { get; set; } // TL, USD, GR vb.
    public string? Description { get; set; }

    public ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();
}