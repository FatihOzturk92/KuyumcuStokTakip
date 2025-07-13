using KuyumcuStokTakip.Application.Common.Interfaces;
using KuyumcuStokTakip.Application.Sales.Common;
using KuyumcuStokTakip.Domain.Entities;
using KuyumcuStokTakip.Domain.Enums;

namespace KuyumcuStokTakip.Application.Sales.Commands.ReturnSale;

public record ReturnSaleCommand : IRequest<Unit>
{
    public DateTime ReturnDate { get; init; } = DateTime.UtcNow;
    public int? CustomerId { get; init; }
    public IList<SaleItemDto> Items { get; init; } = new List<SaleItemDto>();
}

public class ReturnSaleCommandHandler : IRequestHandler<ReturnSaleCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public ReturnSaleCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(ReturnSaleCommand request, CancellationToken cancellationToken)
    {
        foreach (var item in request.Items)
        {
            if (!item.InventoryProductId.HasValue)
                continue;

            var transaction = new StockTransaction
            {
                InventoryProductId = item.InventoryProductId.Value,
                ProductItemId = item.ProductItemId,
                ProductId = item.InventoryProductId.Value,
                CustomerId = request.CustomerId,
                TransactionDate = request.ReturnDate,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                TransactionType = TransactionType.Return,
                Type = EStockTransactionType.In
            };

            _context.StockTransactions.Add(transaction);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
