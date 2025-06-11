using KuyumcuStokTakip.Application.Common.Interfaces;
using KuyumcuStokTakip.Domain.Entities.Account;

namespace KuyumcuStokTakip.Application.Partners.Commands.CreatePartner;

public record CreatePartnerCommand : IRequest<int>
{
    public string Name { get; init; } = string.Empty;
    public string Type { get; init; } = string.Empty;
    public string? PartnerPhone { get; init; }
    public string? PartnerEmail { get; init; }
    public string? PartnerAddress { get; init; }
    public string? Note { get; init; }
}

public class CreatePartnerCommandHandler : IRequestHandler<CreatePartnerCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreatePartnerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreatePartnerCommand request, CancellationToken cancellationToken)
    {
        var entity = new Partner
        {
            Name = request.Name,
            Type = request.Type,
            PartnerPhone = request.PartnerPhone,
            PartnerEmail = request.PartnerEmail,
            PartnerAddress = request.PartnerAddress,
            Note = request.Note
        };

        _context.Partners.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
