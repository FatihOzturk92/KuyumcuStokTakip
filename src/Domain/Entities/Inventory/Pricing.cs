namespace KuyumcuStokTakip.Domain.Entities.Inventory;

public class Pricing
{
    public Guid Id { get; set; }
    public Guid ProductItemId { get; set; }
    public decimal PurchasePrice { get; set; }  // Alış fiyatı
    public decimal SalePrice { get; set; }      // Satış fiyatı
    public DateTime EffectiveDate { get; set; } // Geçerli olduğu tarih
}
