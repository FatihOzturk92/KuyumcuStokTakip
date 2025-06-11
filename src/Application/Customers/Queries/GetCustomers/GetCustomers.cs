using KuyumcuStokTakip.Application.Common.Interfaces;
using KuyumcuStokTakip.Application.Common.Mappings;
using KuyumcuStokTakip.Application.Common.Models;

namespace KuyumcuStokTakip.Application.Customers.Queries.GetCustomers;

public record GetCustomersQuery : IRequest<PaginatedList<CustomerDto>>
{
    public string? SearchTerm { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, PaginatedList<CustomerDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCustomersQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Customers.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query = query.Where(x => x.FirstName.Contains(request.SearchTerm!) || x.LastName.Contains(request.SearchTerm!));
        }

        return await query
            .OrderBy(x => x.FirstName)
            .ThenBy(x => x.LastName)
            .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
