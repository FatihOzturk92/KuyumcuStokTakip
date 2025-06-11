using KuyumcuStokTakip.Application.Common.Interfaces;

namespace KuyumcuStokTakip.Application.StockTransactions.Commands.DeleteStockTransaction;

public record DeleteStockTransactionCommand(int Id) : IRequest;

public class DeleteStockTransactionCommandHandler : IRequestHandler<DeleteStockTransactionCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteStockTransactionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteStockTransactionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.StockTransactions.FindAsync(new object[] { request.Id }, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);

        _context.StockTransactions.Remove(entity!);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
