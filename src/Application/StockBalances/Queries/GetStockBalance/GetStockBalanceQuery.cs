using KuyumcuStokTakip.Application.Common.Interfaces;
using KuyumcuStokTakip.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KuyumcuStokTakip.Application.StockBalances.Queries.GetStockBalance;

public record GetStockBalanceQuery : IRequest<List<StockBalanceDto>>;

public class GetStockBalanceQueryHandler : IRequestHandler<GetStockBalanceQuery, List<StockBalanceDto>>
{
    private readonly IApplicationDbContext _context;

    public GetStockBalanceQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<StockBalanceDto>> Handle(GetStockBalanceQuery request, CancellationToken cancellationToken)
    {
        return await _context.StockTransactions
            .Include(t => t.InventoryProduct)
            .GroupBy(t => new { t.InventoryProductId, t.InventoryProduct.Name })
            .Select(g => new StockBalanceDto
            {
                InventoryProductId = g.Key.InventoryProductId,
                ProductName = g.Key.Name,
                TotalIn = g.Where(t => t.Type == EStockTransactionType.In).Sum(t => t.Quantity),
                TotalOut = g.Where(t => t.Type == EStockTransactionType.Out).Sum(t => t.Quantity),
                NetQuantity = g.Where(t => t.Type == EStockTransactionType.In).Sum(t => t.Quantity) -
                              g.Where(t => t.Type == EStockTransactionType.Out).Sum(t => t.Quantity)
            })
            .OrderBy(x => x.InventoryProductId)
            .ToListAsync(cancellationToken);
    }
}

