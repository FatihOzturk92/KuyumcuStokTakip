using KuyumcuStokTakip.Application.Common.Interfaces;

namespace KuyumcuStokTakip.Application.Inventories.Commands.UpdateInventory;

public record UpdateInventoryCommand : IRequest
{
    public int Id { get; init; }
    public string Code { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public int AccountPartnerId { get; init; }
}

public class UpdateInventoryCommandHandler : IRequestHandler<UpdateInventoryCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateInventoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateInventoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Inventories.FindAsync(new object[] { request.Id }, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);

        entity!.Code = request.Code;
        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.AccountPartnerId = request.AccountPartnerId;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
