namespace KuyumcuStokTakip.Application.StockTransactions.Commands.DeleteStockTransaction;

public class DeleteStockTransactionCommandValidator : AbstractValidator<DeleteStockTransactionCommand>
{
    public DeleteStockTransactionCommandValidator()
    {
        RuleFor(v => v.Id).GreaterThan(0);
    }
}
