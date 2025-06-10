namespace KuyumcuStokTakip.Application.InventoryProducts.Commands.UpdateInventoryProduct;

public class UpdateInventoryProductCommandValidator : AbstractValidator<UpdateInventoryProductCommand>
{
    public UpdateInventoryProductCommandValidator()
    {
        RuleFor(v => v.Id).GreaterThan(0);
        RuleFor(v => v.Code).NotEmpty().MaximumLength(50);
        RuleFor(v => v.Name).NotEmpty().MaximumLength(200);
        RuleFor(v => v.CategoryId).GreaterThan(0);
        RuleFor(v => v.InventoryId).GreaterThan(0);
        RuleFor(v => v.UnitId).GreaterThan(0);
    }
}
