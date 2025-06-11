namespace KuyumcuStokTakip.Application.Inventories.Commands.UpdateInventory;

public class UpdateInventoryCommandValidator : AbstractValidator<UpdateInventoryCommand>
{
    public UpdateInventoryCommandValidator()
    {
        RuleFor(v => v.Id).GreaterThan(0);
        RuleFor(v => v.Code).NotEmpty().MaximumLength(50);
        RuleFor(v => v.Name).NotEmpty().MaximumLength(200);
        RuleFor(v => v.AccountPartnerId).GreaterThan(0);
    }
}
