using KuyumcuStokTakip.Application.Common.Interfaces;

namespace KuyumcuStokTakip.Application.Partners.Commands.UpdatePartner;

public record UpdatePartnerCommand : IRequest
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Type { get; init; } = string.Empty;
    public string? PartnerPhone { get; init; }
    public string? PartnerEmail { get; init; }
    public string? PartnerAddress { get; init; }
    public string? Note { get; init; }
}

public class UpdatePartnerCommandHandler : IRequestHandler<UpdatePartnerCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdatePartnerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdatePartnerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Partners.FindAsync(new object[] { request.Id }, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);

        entity!.Name = request.Name;
        entity.Type = request.Type;
        entity.PartnerPhone = request.PartnerPhone;
        entity.PartnerEmail = request.PartnerEmail;
        entity.PartnerAddress = request.PartnerAddress;
        entity.Note = request.Note;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
