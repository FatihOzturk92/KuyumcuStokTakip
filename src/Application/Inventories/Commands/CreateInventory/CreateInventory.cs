using KuyumcuStokTakip.Application.Common.Interfaces;
using KuyumcuStokTakip.Domain.Entities.Inventory;

namespace KuyumcuStokTakip.Application.Inventories.Commands.CreateInventory;

public record CreateInventoryCommand : IRequest<int>
{
    public string Code { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public int AccountPartnerId { get; init; }
}

public class CreateInventoryCommandHandler : IRequestHandler<CreateInventoryCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateInventoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
    {
        var entity = new Inventory
        {
            Code = request.Code,
            Name = request.Name,
            Description = request.Description,
            AccountPartnerId = request.AccountPartnerId
        };

        _context.Inventories.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
