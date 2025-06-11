using KuyumcuStokTakip.Application.Common.Interfaces;
using KuyumcuStokTakip.Application.Common.Mappings;
using KuyumcuStokTakip.Application.Common.Models;

namespace KuyumcuStokTakip.Application.Inventories.Queries.GetInventories;

public record GetInventoriesQuery : IRequest<PaginatedList<InventoryDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetInventoriesQueryHandler : IRequestHandler<GetInventoriesQuery, PaginatedList<InventoryDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetInventoriesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<InventoryDto>> Handle(GetInventoriesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Inventories
            .OrderBy(x => x.Name)
            .ProjectTo<InventoryDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
