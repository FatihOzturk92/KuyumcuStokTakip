using KuyumcuStokTakip.Domain.Entities.Account;

namespace KuyumcuStokTakip.Domain.Entities.Finance;

public class CashTransaction : BaseAuditableEntity
{
    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

    public decimal Amount { get; set; } // Tutar
    public CashTransactionType TransactionType { get; set; } // Giriş mi, çıkış mı
    public CashTransactionSource Source { get; set; } // Nakit, Banka, POS

    public string? Currency { get; set; } // TL, USD, ALTIN vb.
    public string? Description { get; set; } // Açıklama (örn: Satış tahsilatı)

    public int? CustomerId { get; set; }
    public Customer? Customer { get; set; }

    public int? PartnerId { get; set; }
    public Partner? Partner { get; set; }
}