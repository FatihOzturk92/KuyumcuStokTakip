namespace KuyumcuStokTakip.Application.InventoryProducts.Commands.CreateInventoryProduct;

public class CreateInventoryProductCommandValidator : AbstractValidator<CreateInventoryProductCommand>
{
    public CreateInventoryProductCommandValidator()
    {
        RuleFor(v => v.Code).NotEmpty().MaximumLength(50);
        RuleFor(v => v.Name).NotEmpty().MaximumLength(200);
        RuleFor(v => v.CategoryId).GreaterThan(0);
        RuleFor(v => v.InventoryId).GreaterThan(0);
        RuleFor(v => v.UnitId).GreaterThan(0);
    }
}
