using KuyumcuStokTakip.Domain.Entities.Inventory;

namespace KuyumcuStokTakip.Application.InventoryProducts.Queries.GetInventoryProducts;

public class InventoryProductDto
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

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<InventoryProduct, InventoryProductDto>();
        }
    }
}
