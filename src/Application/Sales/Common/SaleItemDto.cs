using AutoMapper;
using KuyumcuStokTakip.Domain.Entities.Sales;

namespace KuyumcuStokTakip.Application.Sales.Common;

public class SaleItemDto
{
    public int Id { get; init; }
    public int? ProductItemId { get; init; }
    public int? InventoryProductId { get; init; }
    public decimal Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal Total { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<SaleItem, SaleItemDto>();
        }
    }
}
