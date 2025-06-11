using KuyumcuStokTakip.Domain.Entities.Finance;
using KuyumcuStokTakip.Domain.Entities.Inventory;

namespace KuyumcuStokTakip.Domain.Entities.Account;

public class Partner : BaseAuditableEntity
{
    public string Name { get; set; } = default!; // Firma veya kişi adı SevGold
    public string Type { get; set; } = default!; // Toptancı, Atölye, Şube vb.
    public ICollection<AccountPartner> ContactPersons { get; set; } = new List<AccountPartner>(); // Bu kişiler bu partnere bağlı
    public string? PartnerPhone { get; set; }
    public string? PartnerEmail { get; set; }
    public string? PartnerAddress { get; set; }
    public string? Note { get; set; }

    public ICollection<StockTransaction> IncomingTransactions { get; set; } = new List<StockTransaction>();
    public ICollection<StockTransaction> OutgoingTransactions { get; set; } = new List<StockTransaction>();
    public ICollection<AccountTransaction> AccountTransactions { get; set; } = new List<AccountTransaction>();

}