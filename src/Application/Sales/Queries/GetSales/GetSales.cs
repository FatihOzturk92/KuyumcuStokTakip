using KuyumcuStokTakip.Application.Common.Interfaces;
using KuyumcuStokTakip.Application.Common.Mappings;
using KuyumcuStokTakip.Application.Common.Models;

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
        return await _context.Sales
            .OrderByDescending(x => x.SaleDate)
            .ProjectTo<SaleDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
