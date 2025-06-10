namespace KuyumcuStokTakip.Domain.Entities.Finance;

public class Expense : BaseAuditableEntity
{
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public string ExpenseType { get; set; } = default!; // Elektrik, Maaş vb.
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public string? PaymentMethod { get; set; } // Nakit, Banka vb.
    public string? Currency { get; set; } // TL, USD, GR vb.
}