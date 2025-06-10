namespace KuyumcuStokTakip.Application.Expenses.Commands.UpdateExpense;

public class UpdateExpenseCommandValidator : AbstractValidator<UpdateExpenseCommand>
{
    public UpdateExpenseCommandValidator()
    {
        RuleFor(v => v.Id).GreaterThan(0);
        RuleFor(v => v.ExpenseType).NotEmpty();
        RuleFor(v => v.Amount).GreaterThan(0);
    }
}
