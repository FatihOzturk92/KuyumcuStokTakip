using KuyumcuStokTakip.Application.Common.Interfaces;
using KuyumcuStokTakip.Application.Common.Mappings;
using KuyumcuStokTakip.Application.Common.Models;
using KuyumcuStokTakip.Domain.Entities;

namespace KuyumcuStokTakip.Application.Sales.Queries.GetSales;

public record GetSalesQuery : IRequest<PaginatedList<SaleDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetSalesQueryHandler : IRequestHandler<GetSalesQuery, PaginatedList<SaleDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetSalesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<SaleDto>> Handle(GetSalesQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.Sales
            .OrderByDescending(x => x.SaleDate)
            .ProjectTo<SaleDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);

        foreach (var sale in list.Items)
        {
            await AddProfitAsync(sale.Items, cancellationToken);
        }

        return list;
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
