using KuyumcuStokTakip.Application.Common.Interfaces;
using KuyumcuStokTakip.Domain.Entities.Inventory;

namespace KuyumcuStokTakip.Application.InventoryProducts.Commands.CreateInventoryProduct;

public record CreateInventoryProductCommand : IRequest<int>
{
    public string Code { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public int CategoryId { get; init; }
    public int InventoryId { get; init; }
    public int UnitId { get; init; }
    public decimal TotalWeight { get; init; }
    public decimal TotalPieceCount { get; init; }
    public decimal TotalLaborCost { get; init; }
    public decimal TotalMaterialCost { get; init; }
}

public class CreateInventoryProductCommandHandler : IRequestHandler<CreateInventoryProductCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateInventoryProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateInventoryProductCommand request, CancellationToken cancellationToken)
    {
        var entity = new InventoryProduct
        {
            Code = request.Code,
            Name = request.Name,
            Description = request.Description,
            CategoryId = request.CategoryId,
            InventoryId = request.InventoryId,
            UnitId = request.UnitId,
            TotalWeight = request.TotalWeight,
            TotalPieceCount = request.TotalPieceCount,
            TotalLaborCost = request.TotalLaborCost,
            TotalMaterialCost = request.TotalMaterialCost
        };

        _context.InventoryProducts.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
