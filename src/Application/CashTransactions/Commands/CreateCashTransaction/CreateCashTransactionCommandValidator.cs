namespace KuyumcuStokTakip.Application.CashTransactions.Commands.CreateCashTransaction;

public class CreateCashTransactionCommandValidator : AbstractValidator<CreateCashTransactionCommand>
{
    public CreateCashTransactionCommandValidator()
    {
        RuleFor(v => v.Amount).GreaterThan(0);
    }
}
