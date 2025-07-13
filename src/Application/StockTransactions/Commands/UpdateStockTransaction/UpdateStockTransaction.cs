using KuyumcuStokTakip.Application.Common.Interfaces;
using KuyumcuStokTakip.Domain.Entities;
using KuyumcuStokTakip.Domain.Entities.Inventory;
using KuyumcuStokTakip.Domain.Enums;

namespace KuyumcuStokTakip.Application.StockTransactions.Commands.UpdateStockTransaction;

public record UpdateStockTransactionCommand : IRequest
{
    public int Id { get; init; }
    public decimal PureGram { get; init; }
    public decimal PureUnitPrice { get; init; }
    public decimal LaborUnitPrice { get; init; }

    public int InventoryProductId { get; init; }
    public int? ProductItemId { get; init; }
    public int ProductId { get; init; }
    public TransactionType TransactionType { get; init; }

    public DateTime TransactionDate { get; init; } = DateTime.UtcNow;

    public decimal Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal Weight { get; init; }

    public EUnitPriceType UnitPriceType { get; init; }
    public EStockTransactionType Type { get; init; }
    public string? Description { get; init; }
    public string? WastageReason { get; init; }

    public ETransactionSourceType? OutgoingTargetType { get; init; }

    public int? SourceCompanyId { get; init; }
    public int? TargetCompanyId { get; init; }

    public int? CustomerId { get; init; }
    public decimal TotalCost { get; init; }
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

        entity!.PureGram = request.PureGram;
        entity.PureUnitPrice = request.PureUnitPrice;
        entity.LaborUnitPrice = request.LaborUnitPrice;
        entity.InventoryProductId = request.InventoryProductId;
        entity.ProductItemId = request.ProductItemId;
        entity.ProductId = request.ProductId;
        entity.TransactionType = request.TransactionType;
        entity.TransactionDate = request.TransactionDate;
        entity.Quantity = request.Quantity;
        entity.UnitPrice = request.UnitPrice;
        entity.Weight = request.Weight;
        entity.UnitPriceType = request.UnitPriceType;
        entity.Type = request.Type;
        entity.Description = request.Description;
        entity.WastageReason = request.WastageReason;
        entity.OutgoingTargetType = request.OutgoingTargetType;
        entity.SourceCompanyId = request.SourceCompanyId;
        entity.TargetCompanyId = request.TargetCompanyId;
        entity.CustomerId = request.CustomerId;
        entity.TotalCost = request.TotalCost;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
