using KuyumcuStokTakip.Application.Common.Exceptions;
using KuyumcuStokTakip.Application.Expenses.Commands.CreateExpense;
using KuyumcuStokTakip.Domain.Entities.Finance;

namespace KuyumcuStokTakip.Application.FunctionalTests.Expenses.Commands;

using static Testing;

public class CreateExpenseTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireFields()
    {
        var command = new CreateExpenseCommand { Amount = 0, ExpenseType = "" };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateExpense()
    {
        var command = new CreateExpenseCommand
        {
            ExpenseType = "Rent",
            Amount = 100,
            Description = "April rent"
        };

        var id = await SendAsync(command);
        var entity = await FindAsync<Expense>(id);
        entity.Should().NotBeNull();
        entity!.ExpenseType.Should().Be(command.ExpenseType);
        entity.Amount.Should().Be(command.Amount);
    }
}
