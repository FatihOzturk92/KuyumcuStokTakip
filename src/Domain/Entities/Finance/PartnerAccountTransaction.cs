using KuyumcuStokTakip.Domain.Entities.Account;

namespace KuyumcuStokTakip.Domain.Entities.Finance;

public class PartnerAccountTransaction : BaseAuditableEntity
{
    public Guid PartnerId { get; set; }
    public Partner Partner { get; set; } = default!;

    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

    public AccountCurrencyType CurrencyType { get; set; } // Gold, Dollar, TL
    public AccountTransactionType TransactionType { get; set; } // Debit, Credit

    public decimal Amount { get; set; }
    public string? Description { get; set; } // Açıklama (örn: Alım karşılığı altın borcu)
}