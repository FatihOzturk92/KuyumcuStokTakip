using KuyumcuStokTakip.Domain.Entities.Inventory;

namespace KuyumcuStokTakip.Application.Inventories.Queries.GetInventories;

public class InventoryDto
{
    public int Id { get; init; }
    public string Code { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public int AccountPartnerId { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Inventory, InventoryDto>();
        }
    }
}
