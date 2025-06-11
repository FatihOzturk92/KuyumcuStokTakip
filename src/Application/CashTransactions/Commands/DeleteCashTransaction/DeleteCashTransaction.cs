using KuyumcuStokTakip.Application.Common.Interfaces;

namespace KuyumcuStokTakip.Application.CashTransactions.Commands.DeleteCashTransaction;

public record DeleteCashTransactionCommand(int Id) : IRequest;

public class DeleteCashTransactionCommandHandler : IRequestHandler<DeleteCashTransactionCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteCashTransactionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteCashTransactionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.CashTransactions.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity != null)
        {
            _context.CashTransactions.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
