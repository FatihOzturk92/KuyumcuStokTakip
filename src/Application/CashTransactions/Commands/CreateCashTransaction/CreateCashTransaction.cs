using KuyumcuStokTakip.Application.Common.Interfaces;
using KuyumcuStokTakip.Domain.Entities.Finance;
using KuyumcuStokTakip.Domain.Entities;

namespace KuyumcuStokTakip.Application.CashTransactions.Commands.CreateCashTransaction;

public record CreateCashTransactionCommand : IRequest<int>
{
    public DateTime TransactionDate { get; init; } = DateTime.UtcNow;
    public decimal Amount { get; init; }
    public CashTransactionType TransactionType { get; init; }
    public CashTransactionSource Source { get; init; }
    public string? Currency { get; init; }
    public string? Description { get; init; }
    public int? CustomerId { get; init; }
    public int? PartnerId { get; init; }
}

public class CreateCashTransactionCommandHandler : IRequestHandler<CreateCashTransactionCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateCashTransactionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCashTransactionCommand request, CancellationToken cancellationToken)
    {
        var entity = new CashTransaction
        {
            TransactionDate = request.TransactionDate,
            Amount = request.Amount,
            TransactionType = request.TransactionType,
            Source = request.Source,
            Currency = request.Currency,
            Description = request.Description,
            CustomerId = request.CustomerId,
            PartnerId = request.PartnerId
        };

        _context.CashTransactions.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
