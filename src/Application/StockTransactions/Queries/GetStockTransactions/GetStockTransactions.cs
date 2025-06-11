using KuyumcuStokTakip.Application.Common.Interfaces;
using KuyumcuStokTakip.Application.Common.Mappings;
using KuyumcuStokTakip.Application.Common.Models;

namespace KuyumcuStokTakip.Application.StockTransactions.Queries.GetStockTransactions;

public record GetStockTransactionsQuery : IRequest<PaginatedList<StockTransactionDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetStockTransactionsQueryHandler : IRequestHandler<GetStockTransactionsQuery, PaginatedList<StockTransactionDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetStockTransactionsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<StockTransactionDto>> Handle(GetStockTransactionsQuery request, CancellationToken cancellationToken)
    {
        return await _context.StockTransactions
            .OrderByDescending(x => x.TransactionDate)
            .ProjectTo<StockTransactionDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
