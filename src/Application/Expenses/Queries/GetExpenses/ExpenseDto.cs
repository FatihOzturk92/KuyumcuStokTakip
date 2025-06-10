using KuyumcuStokTakip.Domain.Entities.Finance;

namespace KuyumcuStokTakip.Application.Expenses.Queries.GetExpenses;

public class ExpenseDto
{
    public int Id { get; init; }
    public DateTime Date { get; init; }
    public string ExpenseType { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public string? Description { get; init; }
    public string? PaymentMethod { get; init; }
    public string? Currency { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Expense, ExpenseDto>();
        }
    }
}
