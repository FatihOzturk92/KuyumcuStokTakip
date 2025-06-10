namespace KuyumcuStokTakip.Domain.Entities.Inventory;

public class StockUnit : BaseAuditableEntity
{
    public string Name { get; set; } = default!; // Gram, Adet 
    public string Symbol { get; set; } = default!; // örn: g, adet kaldır
    public string? Description { get; set; }

    public ICollection<InventoryProduct> InventoryProducts { get; set; } = new List<InventoryProduct>();
}