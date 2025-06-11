using KuyumcuStokTakip.Application.Common.Interfaces;
using KuyumcuStokTakip.Application.Sales.Queries.GetSales;
using KuyumcuStokTakip.Domain.Entities;

namespace KuyumcuStokTakip.Application.Sales.Queries.GetSaleById;

public record GetSaleByIdQuery(int Id) : IRequest<SaleDto>;

public class GetSaleByIdQueryHandler : IRequestHandler<GetSaleByIdQuery, SaleDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetSaleByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SaleDto> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
    {
        var sale = await _context.Sales
            .Where(x => x.Id == request.Id)
            .ProjectTo<SaleDto>(_mapper.ConfigurationProvider)
            .FirstAsync(cancellationToken);

        await AddProfitAsync(sale.Items, cancellationToken);

        return sale;
    }

    private async Task AddProfitAsync(IEnumerable<Common.SaleItemDto> items, CancellationToken cancellationToken)
    {
        foreach (var item in items)
        {
            if (!item.InventoryProductId.HasValue)
                continue;

            var avgPurchasePrice = await _context.StockTransactions
                .Where(t => t.InventoryProductId == item.InventoryProductId.Value && t.Type == EStockTransactionType.In)
                .Select(t => (decimal?)t.UnitPrice)
                .AverageAsync(cancellationToken);

            if (avgPurchasePrice.HasValue)
            {
                item.Profit = (item.UnitPrice - avgPurchasePrice.Value) * item.Quantity;
            }
        }
    }
}
