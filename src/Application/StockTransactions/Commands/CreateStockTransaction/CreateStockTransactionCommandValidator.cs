namespace KuyumcuStokTakip.Application.StockTransactions.Commands.CreateStockTransaction;

public class CreateStockTransactionCommandValidator : AbstractValidator<CreateStockTransactionCommand>
{
    public CreateStockTransactionCommandValidator()
    {
        RuleFor(v => v.InventoryProductId).GreaterThan(0);
        RuleFor(v => v.Quantity).GreaterThan(0);
        RuleFor(v => v.UnitPrice).GreaterThan(0);
    }
}
