namespace KuyumcuStokTakip.Domain.Entities.Inventory;

public class ProductCategory : BaseAuditableEntity
{
    public string Name { get; set; } = default!; // Sarrafiye (Çeyrek , yarım ,Tam),8K, 14K,18K,22K ,Saat,pırlanta
    public string? Description { get; set; }

    public ICollection<InventoryProduct> InventoryProducts { get; set; } = new List<InventoryProduct>();
}