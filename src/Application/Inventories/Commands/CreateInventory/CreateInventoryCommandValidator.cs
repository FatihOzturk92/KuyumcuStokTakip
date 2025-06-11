namespace KuyumcuStokTakip.Application.Inventories.Commands.CreateInventory;

public class CreateInventoryCommandValidator : AbstractValidator<CreateInventoryCommand>
{
    public CreateInventoryCommandValidator()
    {
        RuleFor(v => v.Code).NotEmpty().MaximumLength(50);
        RuleFor(v => v.Name).NotEmpty().MaximumLength(200);
        RuleFor(v => v.AccountPartnerId).GreaterThan(0);
    }
}
