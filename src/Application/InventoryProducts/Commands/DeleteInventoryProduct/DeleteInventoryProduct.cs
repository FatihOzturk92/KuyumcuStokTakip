using KuyumcuStokTakip.Application.Common.Interfaces;

namespace KuyumcuStokTakip.Application.InventoryProducts.Commands.DeleteInventoryProduct;

public record DeleteInventoryProductCommand(int Id) : IRequest;

public class DeleteInventoryProductCommandHandler : IRequestHandler<DeleteInventoryProductCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteInventoryProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteInventoryProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.InventoryProducts.FindAsync(new object[] { request.Id }, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);

        _context.InventoryProducts.Remove(entity!);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
