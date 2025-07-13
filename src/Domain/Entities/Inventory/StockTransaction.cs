using System;
using KuyumcuStokTakip.Domain.Entities.Account;
using KuyumcuStokTakip.Domain.Entities.Inventory;
using KuyumcuStokTakip.Domain.Enums;

namespace KuyumcuStokTakip.Domain.Entities.Inventory;

public class StockTransaction : BaseAuditableEntity
{
    public decimal PureGram { get; set; }          // Has altın gram miktarı
    public decimal PureUnitPrice { get; set; }     // Has altın gram fiyatı (milyem bazında)
    public decimal LaborUnitPrice { get; set; }    // İşçilik gram fiyatı

    public int InventoryProductId { get; set; }
    public InventoryProduct InventoryProduct { get; set; } = default!;

    public int? ProductItemId { get; set; }
    public ProductItem? ProductItem { get; set; } = default!;

    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

    public decimal Quantity { get; set; } // Adet veya gram
    public decimal UnitPrice { get; set; } // Birim fiyat 0,936
    public decimal Weight { get; set; } // Toplu işlemde tümünün toplam ağırlığı veya değeri

    public int ProductId { get; set; }
    public TransactionType TransactionType { get; set; }

    public EUnitPriceType UnitPriceType { get; set; } // TL, USD, Milyem
    public EStockTransactionType Type { get; set; } // Giriş / Çıkış
    public string? Description { get; set; }
    public string? WastageReason { get; set; }

    public ETransactionSourceType? OutgoingTargetType { get; set; }


    public int? SourceCompanyId { get; set; } // Girişse Toptancı/Atölye ID'si
    public Partner? SourceCompany { get; set; }

    public int? TargetCompanyId { get; set; } // Çıkışsa alıcı atölye veya şube
    public Partner? TargetCompany { get; set; }

    // Çıkışlarda kime gitti? (Customer, Partner veya null → Anonim)

    public int? CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public decimal TotalCost { get; set; }  // Toplam maliyet (yeni eklenen alan)

}