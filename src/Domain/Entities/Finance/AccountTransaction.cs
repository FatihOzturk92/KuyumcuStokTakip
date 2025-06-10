namespace KuyumcuStokTakip.Domain.Entities.Finance;

public class AccountTransaction : BaseAuditableEntity
{
    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

    public AccountOwnerType OwnerType { get; set; } // Müşteri, Partner
    public int OwnerId { get; set; }

    public AccountTransactionType TransactionType { get; set; } // Borç, Alacak
    public AccountCurrencyType CurrencyType { get; set; } // TL, Altın, USD

    public decimal Amount { get; set; }
    public string? Description { get; set; }
}