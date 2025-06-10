using KuyumcuStokTakip.Application.Common.Interfaces;
using KuyumcuStokTakip.Application.Common.Mappings;
using KuyumcuStokTakip.Application.Common.Models;

namespace KuyumcuStokTakip.Application.InventoryProducts.Queries.GetInventoryProducts;

public record GetInventoryProductsQuery : IRequest<PaginatedList<InventoryProductDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetInventoryProductsQueryHandler : IRequestHandler<GetInventoryProductsQuery, PaginatedList<InventoryProductDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetInventoryProductsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<InventoryProductDto>> Handle(GetInventoryProductsQuery request, CancellationToken cancellationToken)
    {
        return await _context.InventoryProducts
            .OrderBy(x => x.Name)
            .ProjectTo<InventoryProductDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
