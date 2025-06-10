using KuyumcuStokTakip.Application.Common.Interfaces;

namespace KuyumcuStokTakip.Application.Expenses.Commands.DeleteExpense;

public record DeleteExpenseCommand(int Id) : IRequest;

public class DeleteExpenseCommandHandler : IRequestHandler<DeleteExpenseCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteExpenseCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Expenses.FindAsync(new object[] { request.Id }, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);

        _context.Expenses.Remove(entity!);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
