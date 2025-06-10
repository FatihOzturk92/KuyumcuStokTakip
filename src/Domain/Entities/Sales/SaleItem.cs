using System.ComponentModel.DataAnnotations.Schema;
using KuyumcuStokTakip.Domain.Entities.Inventory;

namespace KuyumcuStokTakip.Domain.Entities.Sales;
public class SaleItem : BaseAuditableEntity
{
    public Guid SaleId { get; set; }
    public Sale Sale { get; set; } = default!;

    public Guid? ProductItemId { get; set; }
    public ProductItem? ProductItem { get; set; }

    public Guid? InventoryProductId { get; set; }
    public InventoryProduct? InventoryProduct { get; set; }

    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    [NotMapped]
    public decimal Total => Quantity * UnitPrice;
}
