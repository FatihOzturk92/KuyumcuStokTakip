using KuyumcuStokTakip.Application.Common.Interfaces;
using KuyumcuStokTakip.Application.Common.Mappings;
using KuyumcuStokTakip.Application.Common.Models;
using KuyumcuStokTakip.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KuyumcuStokTakip.Application.Sales.Queries.GetSales;

public record GetSalesQuery : IRequest<PaginatedList<SaleDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;

    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public string? CustomerName { get; init; }
    public int? CategoryId { get; init; }
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
        var query = _context.Sales.AsQueryable();

        if (request.StartDate.HasValue)
        {
            query = query.Where(s => s.SaleDate >= request.StartDate.Value);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(s => s.SaleDate <= request.EndDate.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.CustomerName))
        {
            var term = request.CustomerName.ToLower();
            query = query.Where(s => s.Customer != null &&
                EF.Functions.Like((s.Customer.FirstName + " " + s.Customer.LastName).ToLower(), $"%{term}%"));
        }

        if (request.CategoryId.HasValue)
        {
            query = query.Where(s => s.Items.Any(i => i.InventoryProduct != null && i.InventoryProduct.CategoryId == request.CategoryId.Value));
        }

        var list = await query
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
