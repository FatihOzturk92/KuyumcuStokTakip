using KuyumcuStokTakip.Application.Common.Exceptions;
using KuyumcuStokTakip.Application.Expenses.Commands.CreateExpense;
using KuyumcuStokTakip.Application.Expenses.Commands.DeleteExpense;
using KuyumcuStokTakip.Domain.Entities.Finance;

namespace KuyumcuStokTakip.Application.FunctionalTests.Expenses.Commands;

using static Testing;

public class DeleteExpenseTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidId()
    {
        var command = new DeleteExpenseCommand(99);
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteExpense()
    {
        var id = await SendAsync(new CreateExpenseCommand { ExpenseType = "Rent", Amount = 100 });
        await SendAsync(new DeleteExpenseCommand(id));
        var entity = await FindAsync<Expense>(id);
        entity.Should().BeNull();
    }
}
