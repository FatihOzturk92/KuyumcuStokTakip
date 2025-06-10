namespace KuyumcuStokTakip.Domain.Entities.Finance;

public class Currency : BaseAuditableEntity
{
    public string Code { get; set; } = default!; // USD, EUR, GR
    public string Name { get; set; } = default!;
    public string? Symbol { get; set; } // $, €, 🪙
}