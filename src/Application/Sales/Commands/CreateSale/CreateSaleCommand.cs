using KuyumcuStokTakip.Application.Common.Interfaces;
using KuyumcuStokTakip.Domain.Entities.Inventory;
using KuyumcuStokTakip.Application.Sales.Common;
using KuyumcuStokTakip.Domain.Entities.Sales;
using KuyumcuStokTakip.Domain.Enums;
using KuyumcuStokTakip.Domain.Entities;

namespace KuyumcuStokTakip.Application.Sales.Commands.CreateSale;

public record CreateSaleCommand : IRequest<int>
{
    public DateTime SaleDate { get; init; } = DateTime.UtcNow;
    public int? CustomerId { get; init; }
    public IList<Common.SaleItemDto> Items { get; init; } = new List<Common.SaleItemDto>();
}

public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateSaleCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = new Sale
        {
            SaleDate = request.SaleDate,
            CustomerId = request.CustomerId
        };

        foreach (var item in request.Items)
        {
            var saleItem = new SaleItem
            {
                ProductItemId = item.ProductItemId,
                InventoryProductId = item.InventoryProductId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            };
            sale.Items.Add(saleItem);

            if (item.InventoryProductId.HasValue)
            {
                var transaction = new StockTransaction
                {
                    InventoryProductId = item.InventoryProductId.Value,
                    ProductItemId = item.ProductItemId,
                    ProductId = item.InventoryProductId.Value,
                    CustomerId = request.CustomerId,
                    TransactionDate = request.SaleDate,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    TransactionType = TransactionType.Sale,
                    Type = EStockTransactionType.Out
                };
                _context.StockTransactions.Add(transaction);
            }
        }

        _context.Sales.Add(sale);
        await _context.SaveChangesAsync(cancellationToken);

        return sale.Id;
    }
}
