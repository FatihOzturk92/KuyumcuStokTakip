using System;
using System.Collections.Generic;
using KuyumcuStokTakip.Domain.Entities.Inventory;

namespace KuyumcuStokTakip.Domain.Entities.Inventory;

public class InventoryProduct : BaseAuditableEntity
{
    //depo , 150 ,151 ,200 , ,150
    
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

    public Guid CategoryId { get; set; }
    public ProductCategory Category { get; set; } = default!;

    public Guid InventoryId { get; set; }// bir kuyumcu için bir id var 
    public Inventory Inventory { get; set; } = default!;

    public Guid UnitId { get; set; }
    public StockUnit Unit { get; set; } = default!;

    public decimal TotalWeight { get; set; } // Bu ürün grubunun toplam gramajı
    public decimal TotalPieceCount { get; set; } // Bu ürün grubunun toplam adedi (isteğe bağlı)
    public decimal TotalLaborCost { get; set; } // İşçilik toplam tutarı (TL / Dolar) 0,934 0,936 0,935 
    public decimal TotalMaterialCost { get; set; } // Saf altın veya döviz toplam maliyeti
    public decimal TotalCost => TotalLaborCost + TotalMaterialCost; // Tam maliyet (hesaplanan)

    public ICollection<StockTransaction> Transactions { get; set; } = new List<StockTransaction>();
}
