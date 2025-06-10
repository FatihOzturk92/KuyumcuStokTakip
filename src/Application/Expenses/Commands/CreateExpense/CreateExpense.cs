using KuyumcuStokTakip.Application.Common.Interfaces;
using KuyumcuStokTakip.Domain.Entities.Finance;

namespace KuyumcuStokTakip.Application.Expenses.Commands.CreateExpense;

public record CreateExpenseCommand : IRequest<int>
{
    public DateTime Date { get; init; } = DateTime.UtcNow;
    public string ExpenseType { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public string? Description { get; init; }
    public string? PaymentMethod { get; init; }
    public string? Currency { get; init; }
}

public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateExpenseCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        var entity = new Expense
        {
            Date = request.Date,
            ExpenseType = request.ExpenseType,
            Amount = request.Amount,
            Description = request.Description,
            PaymentMethod = request.PaymentMethod,
            Currency = request.Currency
        };

        _context.Expenses.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
