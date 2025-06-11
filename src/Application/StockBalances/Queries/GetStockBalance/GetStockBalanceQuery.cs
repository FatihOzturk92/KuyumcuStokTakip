using KuyumcuStokTakip.Application.Common.Interfaces;
using KuyumcuStokTakip.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KuyumcuStokTakip.Application.StockBalances.Queries.GetStockBalance;

public record GetStockBalanceQuery : IRequest<List<StockBalanceDto>>
{
    public string? SearchTerm { get; init; }
}

public class GetStockBalanceQueryHandler : IRequestHandler<GetStockBalanceQuery, List<StockBalanceDto>>
{
    private readonly IApplicationDbContext _context;

    public GetStockBalanceQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<StockBalanceDto>> Handle(GetStockBalanceQuery request, CancellationToken cancellationToken)
    {
        var query = _context.StockTransactions
            .Include(t => t.InventoryProduct)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query = query.Where(t => t.InventoryProduct.Name.Contains(request.SearchTerm!));
        }

        return await query
            .GroupBy(t => new { t.InventoryProductId, t.InventoryProduct.Name })
            .Select(g => new StockBalanceDto
            {
                ProductId = g.Key.InventoryProductId,
                ProductName = g.Key.Name,
                TotalIn = g.Where(t => t.Type == EStockTransactionType.In).Sum(t => t.Quantity),
                TotalOut = g.Where(t => t.Type == EStockTransactionType.Out).Sum(t => t.Quantity),
                Net = g.Where(t => t.Type == EStockTransactionType.In).Sum(t => t.Quantity) -
                      g.Where(t => t.Type == EStockTransactionType.Out).Sum(t => t.Quantity)
            })
            .OrderBy(x => x.ProductId)
            .ToListAsync(cancellationToken);
    }
}

