using KuyumcuStokTakip.Application.Common.Interfaces;

namespace KuyumcuStokTakip.Application.Inventories.Commands.DeleteInventory;

public record DeleteInventoryCommand(int Id) : IRequest;

public class DeleteInventoryCommandHandler : IRequestHandler<DeleteInventoryCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteInventoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteInventoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Inventories.FindAsync(new object[] { request.Id }, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);

        _context.Inventories.Remove(entity!);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
