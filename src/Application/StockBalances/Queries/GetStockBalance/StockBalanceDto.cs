using KuyumcuStokTakip.Domain.Entities.Inventory;
using KuyumcuStokTakip.Domain.Entities;

namespace KuyumcuStokTakip.Application.StockBalances.Queries.GetStockBalance;

public class StockBalanceDto
{
    public int ProductId { get; init; }
    public string ProductName { get; init; } = string.Empty;
    public decimal TotalIn { get; init; }
    public decimal TotalOut { get; init; }
    public decimal Net { get; init; }
}
