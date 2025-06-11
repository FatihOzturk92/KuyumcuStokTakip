using KuyumcuStokTakip.Application.Common.Interfaces;
using KuyumcuStokTakip.Domain.Entities.Inventory;

namespace KuyumcuStokTakip.Application.StockTransactions.Commands.CreateStockTransaction;

public record CreateStockTransactionCommand : IRequest<int>
{
    public int InventoryProductId { get; init; }
    public DateTime TransactionDate { get; init; } = DateTime.UtcNow;
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
}

public class CreateStockTransactionCommandHandler : IRequestHandler<CreateStockTransactionCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateStockTransactionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateStockTransactionCommand request, CancellationToken cancellationToken)
    {
        var entity = new StockTransaction
        {
            InventoryProductId = request.InventoryProductId,
            TransactionDate = request.TransactionDate,
            Quantity = request.Quantity,
            UnitPrice = request.UnitPrice,
            Weight = request.Weight,
            PureGram = request.PureGram,
            PureUnitPrice = request.PureUnitPrice,
            LaborUnitPrice = request.LaborUnitPrice,
            UnitPriceType = request.UnitPriceType,
            Type = request.Type,
            TotalCost = request.TotalCost,
            Description = request.Description
        };

        _context.StockTransactions.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
