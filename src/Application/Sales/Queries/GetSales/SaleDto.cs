using KuyumcuStokTakip.Domain.Entities;
using KuyumcuStokTakip.Domain.Entities.Sales;

namespace KuyumcuStokTakip.Application.Sales.Queries.GetSales;

public class SaleDto
{
    public SaleDto()
    {
        Items = Array.Empty<SaleItemDto>();
    }

    public int Id { get; init; }
    public DateTime SaleDate { get; init; }
    public int? CustomerId { get; init; }
    public EPaymentType PaymentMethod { get; init; }
    public string? Currency { get; init; }
    public string? Description { get; init; }
    public IReadOnlyCollection<SaleItemDto> Items { get; init; }

    public class SaleItemDto
    {
        public int Id { get; init; }
        public int? ProductItemId { get; init; }
        public int? InventoryProductId { get; init; }
        public decimal Quantity { get; init; }
        public decimal UnitPrice { get; init; }
        public decimal Total { get; init; }
    }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<SaleItem, SaleItemDto>();
            CreateMap<Sale, SaleDto>();
        }
    }
}
