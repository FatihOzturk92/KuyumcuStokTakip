using KuyumcuStokTakip.Application.Common.Interfaces;

namespace KuyumcuStokTakip.Application.InventoryProducts.Commands.UpdateInventoryProduct;

public record UpdateInventoryProductCommand : IRequest
{
    public int Id { get; init; }
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

public class UpdateInventoryProductCommandHandler : IRequestHandler<UpdateInventoryProductCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateInventoryProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateInventoryProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.InventoryProducts.FindAsync(new object[] { request.Id }, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);

        entity!.Code = request.Code;
        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.CategoryId = request.CategoryId;
        entity.InventoryId = request.InventoryId;
        entity.UnitId = request.UnitId;
        entity.TotalWeight = request.TotalWeight;
        entity.TotalPieceCount = request.TotalPieceCount;
        entity.TotalLaborCost = request.TotalLaborCost;
        entity.TotalMaterialCost = request.TotalMaterialCost;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
