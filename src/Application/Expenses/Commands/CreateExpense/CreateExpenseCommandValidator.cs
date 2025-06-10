namespace KuyumcuStokTakip.Application.Expenses.Commands.CreateExpense;

public class CreateExpenseCommandValidator : AbstractValidator<CreateExpenseCommand>
{
    public CreateExpenseCommandValidator()
    {
        RuleFor(v => v.ExpenseType).NotEmpty();
        RuleFor(v => v.Amount).GreaterThan(0);
    }
}
