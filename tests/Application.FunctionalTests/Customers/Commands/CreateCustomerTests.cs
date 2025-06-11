using KuyumcuStokTakip.Application.Common.Exceptions;
using KuyumcuStokTakip.Application.Customers.Commands.CreateCustomer;
using KuyumcuStokTakip.Domain.Entities.Account;

namespace KuyumcuStokTakip.Application.FunctionalTests.Customers.Commands;

using static Testing;

public class CreateCustomerTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireFields()
    {
        var command = new CreateCustomerCommand { FirstName = "", LastName = "" };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateCustomer()
    {
        var command = new CreateCustomerCommand
        {
            FirstName = "John",
            LastName = "Doe"
        };

        var id = await SendAsync(command);
        var entity = await FindAsync<Customer>(id);
        entity.Should().NotBeNull();
        entity!.FirstName.Should().Be(command.FirstName);
    }
}
