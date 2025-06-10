using KuyumcuStokTakip.Domain.Entities.Finance;
using KuyumcuStokTakip.Domain.Entities.Inventory;

namespace KuyumcuStokTakip.Domain.Entities.Common;

public class Branch : BaseAuditableEntity
{
    public string Name { get; set; } = default!;
    public string? Location { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public ICollection<InventoryProduct> InventoryProducts { get; set; } = new List<InventoryProduct>();
    public ICollection<CashTransaction> CashTransactions { get; set; } = new List<CashTransaction>();
}