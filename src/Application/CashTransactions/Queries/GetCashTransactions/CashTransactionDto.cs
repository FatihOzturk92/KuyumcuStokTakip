using KuyumcuStokTakip.Domain.Entities.Finance;
using KuyumcuStokTakip.Domain.Entities;

namespace KuyumcuStokTakip.Application.CashTransactions.Queries.GetCashTransactions;

public class CashTransactionDto
{
    public int Id { get; init; }
    public DateTime TransactionDate { get; init; }
    public decimal Amount { get; init; }
    public CashTransactionType TransactionType { get; init; }
    public CashTransactionSource Source { get; init; }
    public string? Currency { get; init; }
    public string? Description { get; init; }
    public int? CustomerId { get; init; }
    public int? PartnerId { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CashTransaction, CashTransactionDto>();
        }
    }
}
