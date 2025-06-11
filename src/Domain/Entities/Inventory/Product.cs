using KuyumcuStokTakip.Domain.Common;
using KuyumcuStokTakip.Domain.Enums;

namespace KuyumcuStokTakip.Domain.Entities.Inventory;

public class Product : BaseAuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public ProductKind ProductType { get; set; }
    public PurityLevel Purity { get; set; }
    public string? ModelName { get; set; }
    public ProductTrackingType TrackingType { get; set; }
}
