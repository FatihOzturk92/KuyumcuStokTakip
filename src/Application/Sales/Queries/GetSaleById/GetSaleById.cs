using KuyumcuStokTakip.Application.Common.Interfaces;
using KuyumcuStokTakip.Application.Sales.Queries.GetSales;

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
        return await _context.Sales
            .Where(x => x.Id == request.Id)
            .ProjectTo<SaleDto>(_mapper.ConfigurationProvider)
            .FirstAsync(cancellationToken);
    }
}
