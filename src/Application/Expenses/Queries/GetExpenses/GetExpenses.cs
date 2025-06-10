using KuyumcuStokTakip.Application.Common.Interfaces;
using KuyumcuStokTakip.Application.Common.Mappings;
using KuyumcuStokTakip.Application.Common.Models;

namespace KuyumcuStokTakip.Application.Expenses.Queries.GetExpenses;

public record GetExpensesQuery : IRequest<PaginatedList<ExpenseDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetExpensesQueryHandler : IRequestHandler<GetExpensesQuery, PaginatedList<ExpenseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetExpensesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ExpenseDto>> Handle(GetExpensesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Expenses
            .OrderByDescending(x => x.Date)
            .ProjectTo<ExpenseDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
