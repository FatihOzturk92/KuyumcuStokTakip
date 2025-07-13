using KuyumcuStokTakip.Domain.Entities.Inventory;
using KuyumcuStokTakip.Domain.Entities;
using KuyumcuStokTakip.Domain.Enums;

namespace KuyumcuStokTakip.Application.StockTransactions.Queries.GetStockTransactions;

public class StockTransactionDto
{
    public int Id { get; init; }
    public decimal PureGram { get; init; }
    public decimal PureUnitPrice { get; init; }
    public decimal LaborUnitPrice { get; init; }
    public int InventoryProductId { get; init; }
    public int? ProductItemId { get; init; }
    public DateTime TransactionDate { get; init; }
    public decimal Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal Weight { get; init; }
    public int ProductId { get; init; }
    public TransactionType TransactionType { get; init; }
    public EUnitPriceType UnitPriceType { get; init; }
    public EStockTransactionType Type { get; init; }
    public string? Description { get; init; }
    public string? WastageReason { get; init; }
    public ETransactionSourceType? OutgoingTargetType { get; init; }
    public int? SourceCompanyId { get; init; }
    public int? TargetCompanyId { get; init; }
    public int? CustomerId { get; init; }
    public decimal TotalCost { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<StockTransaction, StockTransactionDto>();
        }
    }
}
