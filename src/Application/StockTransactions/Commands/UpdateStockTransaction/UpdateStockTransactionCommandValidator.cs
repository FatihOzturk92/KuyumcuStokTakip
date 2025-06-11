namespace KuyumcuStokTakip.Application.StockTransactions.Commands.UpdateStockTransaction;

public class UpdateStockTransactionCommandValidator : AbstractValidator<UpdateStockTransactionCommand>
{
    public UpdateStockTransactionCommandValidator()
    {
        RuleFor(v => v.Id).GreaterThan(0);
        RuleFor(v => v.InventoryProductId).GreaterThan(0);
        RuleFor(v => v.Quantity).GreaterThan(0);
        RuleFor(v => v.UnitPrice).GreaterThanOrEqualTo(0);
        RuleFor(v => v.Weight).GreaterThanOrEqualTo(0);
        RuleFor(v => v.PureGram).GreaterThanOrEqualTo(0);
        RuleFor(v => v.PureUnitPrice).GreaterThanOrEqualTo(0);
        RuleFor(v => v.LaborUnitPrice).GreaterThanOrEqualTo(0);
        RuleFor(v => v.TotalCost).GreaterThanOrEqualTo(0);
    }
}
