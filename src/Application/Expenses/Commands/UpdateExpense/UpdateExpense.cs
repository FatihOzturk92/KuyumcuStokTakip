using KuyumcuStokTakip.Application.Common.Interfaces;

namespace KuyumcuStokTakip.Application.Expenses.Commands.UpdateExpense;

public record UpdateExpenseCommand : IRequest
{
    public int Id { get; init; }
    public DateTime Date { get; init; } = DateTime.UtcNow;
    public string ExpenseType { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public string? Description { get; init; }
    public string? PaymentMethod { get; init; }
    public string? Currency { get; init; }
}

public class UpdateExpenseCommandHandler : IRequestHandler<UpdateExpenseCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateExpenseCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Expenses.FindAsync(new object[] { request.Id }, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);

        entity!.Date = request.Date;
        entity.ExpenseType = request.ExpenseType;
        entity.Amount = request.Amount;
        entity.Description = request.Description;
        entity.PaymentMethod = request.PaymentMethod;
        entity.Currency = request.Currency;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
