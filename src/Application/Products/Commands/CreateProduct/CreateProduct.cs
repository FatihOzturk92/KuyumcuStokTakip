using KuyumcuStokTakip.Application.Common.Interfaces;
using KuyumcuStokTakip.Domain.Entities.Inventory;
using KuyumcuStokTakip.Domain.Enums;

namespace KuyumcuStokTakip.Application.Products.Commands.CreateProduct;

public record CreateProductCommand : IRequest<int>
{
    public string Name { get; init; } = string.Empty;
    public ProductKind ProductType { get; init; }
    public PurityLevel Purity { get; init; }
    public string? ModelName { get; init; }
    public ProductTrackingType TrackingType { get; init; }
    public string? PhotoUrl { get; init; }
    public string? CertificateNumber { get; init; }
    public string? Notes { get; init; }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = new Product
        {
            Name = request.Name,
            ProductType = request.ProductType,
            Purity = request.Purity,
            ModelName = request.ModelName,
            TrackingType = request.TrackingType,
            PhotoUrl = request.PhotoUrl,
            CertificateNumber = request.CertificateNumber,
            Notes = request.Notes
        };

        _context.Products.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
