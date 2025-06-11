using KuyumcuStokTakip.Application.Common.Interfaces;

namespace KuyumcuStokTakip.Application.StockTransactions.Commands.UpdateStockTransaction;

public record UpdateStockTransactionCommand : IRequest
{
    public int Id { get; init; }
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

public class UpdateStockTransactionCommandHandler : IRequestHandler<UpdateStockTransactionCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateStockTransactionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateStockTransactionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.StockTransactions.FindAsync(new object[] { request.Id }, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);

        entity!.InventoryProductId = request.InventoryProductId;
        entity.TransactionDate = request.TransactionDate;
        entity.Quantity = request.Quantity;
        entity.UnitPrice = request.UnitPrice;
        entity.Weight = request.Weight;
        entity.PureGram = request.PureGram;
        entity.PureUnitPrice = request.PureUnitPrice;
        entity.LaborUnitPrice = request.LaborUnitPrice;
        entity.UnitPriceType = request.UnitPriceType;
        entity.Type = request.Type;
        entity.TotalCost = request.TotalCost;
        entity.Description = request.Description;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
