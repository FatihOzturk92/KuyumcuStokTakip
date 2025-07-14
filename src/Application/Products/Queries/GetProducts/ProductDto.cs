using AutoMapper;
using KuyumcuStokTakip.Domain.Entities.Inventory;
using KuyumcuStokTakip.Domain.Enums;

namespace KuyumcuStokTakip.Application.Products.Queries.GetProducts;

public class ProductDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public ProductKind ProductType { get; init; }
    public PurityLevel Purity { get; init; }
    public string? ModelName { get; init; }
    public ProductTrackingType TrackingType { get; init; }
    public string? PhotoUrl { get; init; }
    public string? CertificateNumber { get; init; }
    public string? Notes { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}
