using KuyumcuStokTakip.Application.Common.Exceptions;
using KuyumcuStokTakip.Application.Customers.Commands.CreateCustomer;
using KuyumcuStokTakip.Application.Customers.Commands.DeleteCustomer;
using KuyumcuStokTakip.Domain.Entities.Account;

namespace KuyumcuStokTakip.Application.FunctionalTests.Customers.Commands;

using static Testing;

public class DeleteCustomerTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidId()
    {
        var command = new DeleteCustomerCommand(99);
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteCustomer()
    {
        var id = await SendAsync(new CreateCustomerCommand { FirstName = "John", LastName = "Doe" });
        await SendAsync(new DeleteCustomerCommand(id));
        var entity = await FindAsync<Customer>(id);
        entity.Should().BeNull();
    }
}
