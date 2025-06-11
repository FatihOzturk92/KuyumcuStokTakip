using KuyumcuStokTakip.Application.Common.Interfaces;
using KuyumcuStokTakip.Application.Common.Mappings;
using KuyumcuStokTakip.Application.Common.Models;

namespace KuyumcuStokTakip.Application.CashTransactions.Queries.GetCashTransactions;

public record GetCashTransactionsQuery : IRequest<PaginatedList<CashTransactionDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetCashTransactionsQueryHandler : IRequestHandler<GetCashTransactionsQuery, PaginatedList<CashTransactionDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCashTransactionsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CashTransactionDto>> Handle(GetCashTransactionsQuery request, CancellationToken cancellationToken)
    {
        return await _context.CashTransactions
            .OrderByDescending(x => x.TransactionDate)
            .ProjectTo<CashTransactionDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
