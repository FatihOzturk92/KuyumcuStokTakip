using KuyumcuStokTakip.Application.Common.Interfaces;
using KuyumcuStokTakip.Application.Common.Mappings;
using KuyumcuStokTakip.Application.Common.Models;

namespace KuyumcuStokTakip.Application.Partners.Queries.GetPartners;

public record GetPartnersQuery : IRequest<PaginatedList<PartnerDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetPartnersQueryHandler : IRequestHandler<GetPartnersQuery, PaginatedList<PartnerDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPartnersQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<PartnerDto>> Handle(GetPartnersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Partners
            .OrderBy(x => x.Name)
            .ProjectTo<PartnerDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
