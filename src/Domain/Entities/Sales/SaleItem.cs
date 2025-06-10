using KuyumcuStokTakip.Domain.Entities.Inventory;

namespace KuyumcuStokTakip.Domain.Entities.Sales;

public class SaleItem : BaseAuditableEntity
{
    public Guid SaleId { get; set; }
    public Sale Sale { get; set; } = default!;

    public Guid? ProductItemId { get; set; }
    public ProductItem? ProductItems { get; set; } = default!;

    public Guid InventoryProductId { get; set; }
    public InventoryProduct InventoryProducts { get; set; } = default!;

    public decimal Quantity { get; set; }// adet 
    public decimal UnitPrice { get; set; } //5000
    public decimal Total => Quantity * UnitPrice;
}