using KuyumcuStokTakip.Application.Common.Interfaces;
using KuyumcuStokTakip.Domain.Entities;
using KuyumcuStokTakip.Domain.Entities.Inventory;
using KuyumcuStokTakip.Domain.Enums;

namespace KuyumcuStokTakip.Application.StockTransactions.Commands.CreateStockTransaction;

public record CreateStockTransactionCommand : IRequest<int>
{
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

public class CreateStockTransactionCommandHandler : IRequestHandler<CreateStockTransactionCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateStockTransactionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateStockTransactionCommand request, CancellationToken cancellationToken)
    {
        // Enforce stock movement direction based on special transaction types
        var type = request.Type;
        if (request.TransactionType == TransactionType.Wastage)
        {
            type = EStockTransactionType.Out;
        }
        else if (request.TransactionType == TransactionType.Return)
        {
            type = EStockTransactionType.In;
        }

        // Exchange requires two separate transactions: one out and one in
        if (request.TransactionType == TransactionType.Exchange)
        {
            var outEntity = new StockTransaction
            {
                PureGram = request.PureGram,
                PureUnitPrice = request.PureUnitPrice,
                LaborUnitPrice = request.LaborUnitPrice,
                InventoryProductId = request.InventoryProductId,
                ProductItemId = request.ProductItemId,
                ProductId = request.ProductId,
                TransactionType = request.TransactionType,
                TransactionDate = request.TransactionDate,
                Quantity = request.Quantity,
                UnitPrice = request.UnitPrice,
                Weight = request.Weight,
                UnitPriceType = request.UnitPriceType,
                Type = EStockTransactionType.Out,
                Description = request.Description,
                WastageReason = request.WastageReason,
                OutgoingTargetType = request.OutgoingTargetType,
                SourceCompanyId = request.SourceCompanyId,
                TargetCompanyId = request.TargetCompanyId,
                CustomerId = request.CustomerId,
                TotalCost = request.TotalCost
            };

            var inEntity = new StockTransaction
            {
                PureGram = request.PureGram,
                PureUnitPrice = request.PureUnitPrice,
                LaborUnitPrice = request.LaborUnitPrice,
                InventoryProductId = request.InventoryProductId,
                ProductItemId = request.ProductItemId,
                ProductId = request.ProductId,
                TransactionType = request.TransactionType,
                TransactionDate = request.TransactionDate,
                Quantity = request.Quantity,
                UnitPrice = request.UnitPrice,
                Weight = request.Weight,
                UnitPriceType = request.UnitPriceType,
                Type = EStockTransactionType.In,
                Description = request.Description,
                WastageReason = request.WastageReason,
                OutgoingTargetType = request.OutgoingTargetType,
                SourceCompanyId = request.SourceCompanyId,
                TargetCompanyId = request.TargetCompanyId,
                CustomerId = request.CustomerId,
                TotalCost = request.TotalCost
            };

            _context.StockTransactions.AddRange(outEntity, inEntity);
            await _context.SaveChangesAsync(cancellationToken);
            return inEntity.Id;
        }
        else
        {
            var entity = new StockTransaction
            {
                PureGram = request.PureGram,
                PureUnitPrice = request.PureUnitPrice,
                LaborUnitPrice = request.LaborUnitPrice,
                InventoryProductId = request.InventoryProductId,
                ProductItemId = request.ProductItemId,
                ProductId = request.ProductId,
                TransactionType = request.TransactionType,
                TransactionDate = request.TransactionDate,
                Quantity = request.Quantity,
                UnitPrice = request.UnitPrice,
                Weight = request.Weight,
                UnitPriceType = request.UnitPriceType,
                Type = type,
                Description = request.Description,
                WastageReason = request.WastageReason,
                OutgoingTargetType = request.OutgoingTargetType,
                SourceCompanyId = request.SourceCompanyId,
                TargetCompanyId = request.TargetCompanyId,
                CustomerId = request.CustomerId,
                TotalCost = request.TotalCost
            };

            _context.StockTransactions.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
