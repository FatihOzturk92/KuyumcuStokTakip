namespace KuyumcuStokTakip.Application.StockTransactions.Commands.UpdateStockTransaction;

public class UpdateStockTransactionCommandValidator : AbstractValidator<UpdateStockTransactionCommand>
{
    public UpdateStockTransactionCommandValidator()
    {
        RuleFor(v => v.Id).GreaterThan(0);
        RuleFor(v => v.InventoryProductId).GreaterThan(0);
        RuleFor(v => v.Quantity).GreaterThan(0);
        RuleFor(v => v.UnitPrice).GreaterThan(0);
    }
}
