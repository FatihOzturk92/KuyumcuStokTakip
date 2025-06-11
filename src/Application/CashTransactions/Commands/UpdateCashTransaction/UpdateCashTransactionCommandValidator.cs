namespace KuyumcuStokTakip.Application.CashTransactions.Commands.UpdateCashTransaction;

public class UpdateCashTransactionCommandValidator : AbstractValidator<UpdateCashTransactionCommand>
{
    public UpdateCashTransactionCommandValidator()
    {
        RuleFor(v => v.Amount).GreaterThan(0);
    }
}
