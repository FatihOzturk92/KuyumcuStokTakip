using System;
using System.Collections.Generic;
using KuyumcuStokTakip.Domain.Entities.Account;
using KuyumcuStokTakip.Domain.Entities.Inventory;

namespace KuyumcuStokTakip.Domain.Entities.Inventory;

public class Inventory : BaseAuditableEntity
{
    //depo , 150 ,151 ,200 , ,150
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

    public int AccountPartnerId { get; set; }
    public AccountPartner AccountPartner { get; set; } = default!; // atölyedeki depolar
    public ICollection<StockTransaction> Transactions { get; set; } = new List<StockTransaction>();
}
