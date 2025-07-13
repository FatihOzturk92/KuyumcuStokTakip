namespace KuyumcuStokTakip.Application.Sales.Commands.ReturnSale;

public class ReturnSaleCommandValidator : AbstractValidator<ReturnSaleCommand>
{
    public ReturnSaleCommandValidator()
    {
        RuleFor(v => v.Items).NotEmpty();
        RuleForEach(v => v.Items).ChildRules(items =>
        {
            items.RuleFor(i => i.Quantity).GreaterThan(0);
            items.RuleFor(i => i.UnitPrice).GreaterThanOrEqualTo(0);
        });
    }
}
