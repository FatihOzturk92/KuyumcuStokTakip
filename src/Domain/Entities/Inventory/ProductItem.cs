using KuyumcuStokTakip.Domain.Entities.Inventory;

public class ProductItem : BaseAuditableEntity
{


    public ProductType ProductType { get; set; } // Ürün tipi (model bazlı vs.)

    public Guid InventoryProductId { get; set; }
    public InventoryProduct InventoryProduct { get; set; } = default!;

    public Guid? StockTransactionId { get; set; } // hangi girişten geldi
    public StockTransaction? StockTransaction { get; set; }

    public string Barcode { get; set; } = default!;
    public decimal Weight { get; set; } // 12.56g
    public EWeightUnit WeightUnit { get; set; } // Gram, Dolar, Adet gibi
    public string? Size { get; set; }
    public string? Description { get; set; }
    public decimal LaborCost { get; set; }// Her bir ürünün işçiligi 0.936  o gün işçilige 0,930 da vermiş olabiliriz 
    public decimal Cost { get; set; }  // Ürün net maliyeti (gram başı işçilik hariç saf altın maliyeti gibi)

    public bool IsSold { get; set; } = false;
    public Guid? PurityId { get; set; }    // Ayar bilgisi
    public Purity? Purity { get; set; }
    public DateTime? SoldDate { get; set; }

}
