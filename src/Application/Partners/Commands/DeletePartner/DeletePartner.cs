using KuyumcuStokTakip.Application.Common.Interfaces;

namespace KuyumcuStokTakip.Application.Partners.Commands.DeletePartner;

public record DeletePartnerCommand(int Id) : IRequest;

public class DeletePartnerCommandHandler : IRequestHandler<DeletePartnerCommand>
{
    private readonly IApplicationDbContext _context;

    public DeletePartnerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeletePartnerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Partners.FindAsync(new object[] { request.Id }, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);

        _context.Partners.Remove(entity!);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
