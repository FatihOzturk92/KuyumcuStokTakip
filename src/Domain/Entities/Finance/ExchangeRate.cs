namespace KuyumcuStokTakip.Domain.Entities.Finance;

public class ExchangeRate : BaseAuditableEntity
{
    public Guid CurrencyId { get; set; }
    public Currency Currency { get; set; } = default!;
    public DateTime Date { get; set; }
    public decimal BuyRate { get; set; }
    public decimal SellRate { get; set; }
}