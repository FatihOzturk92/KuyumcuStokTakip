using KuyumcuStokTakip.Domain.Entities.Inventory;

namespace KuyumcuStokTakip.Application.StockTransactions.Queries.GetStockTransactions;

public class StockTransactionDto
{
    public int Id { get; init; }
    public int InventoryProductId { get; init; }
    public DateTime TransactionDate { get; init; }
    public decimal Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal Weight { get; init; }
    public decimal PureGram { get; init; }
    public decimal PureUnitPrice { get; init; }
    public decimal LaborUnitPrice { get; init; }
    public EUnitPriceType UnitPriceType { get; init; }
    public EStockTransactionType Type { get; init; }
    public decimal TotalCost { get; init; }
    public string? Description { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<StockTransaction, StockTransactionDto>();
        }
    }
}
