using KuyumcuStokTakip.Application.Common.Interfaces;
using KuyumcuStokTakip.Domain.Entities.Finance;
using KuyumcuStokTakip.Domain.Entities;

namespace KuyumcuStokTakip.Application.CashTransactions.Commands.UpdateCashTransaction;

public record UpdateCashTransactionCommand : IRequest
{
    public int Id { get; init; }
    public DateTime TransactionDate { get; init; } = DateTime.UtcNow;
    public decimal Amount { get; init; }
    public CashTransactionType TransactionType { get; init; }
    public CashTransactionSource Source { get; init; }
    public string? Currency { get; init; }
    public string? Description { get; init; }
    public int? CustomerId { get; init; }
    public int? PartnerId { get; init; }
}

public class UpdateCashTransactionCommandHandler : IRequestHandler<UpdateCashTransactionCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCashTransactionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateCashTransactionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.CashTransactions.FindAsync(new object[] { request.Id }, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);

        entity!.TransactionDate = request.TransactionDate;
        entity.Amount = request.Amount;
        entity.TransactionType = request.TransactionType;
        entity.Source = request.Source;
        entity.Currency = request.Currency;
        entity.Description = request.Description;
        entity.CustomerId = request.CustomerId;
        entity.PartnerId = request.PartnerId;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
