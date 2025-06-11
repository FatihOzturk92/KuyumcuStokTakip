using AutoMapper;
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
    public string CustomerName { get; init; } = string.Empty;
    public EPaymentType PaymentMethod { get; init; }
    public string? Currency { get; init; }
    public string? Description { get; init; }
    public decimal TotalAmount { get; init; }
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
            CreateMap<Sale, SaleDto>()
                .ForMember(d => d.CustomerName,
                    opt => opt.MapFrom(s => s.Customer != null
                        ? string.Concat(s.Customer.FirstName, " ", s.Customer.LastName)
                        : string.Empty));
        }
    }
}
