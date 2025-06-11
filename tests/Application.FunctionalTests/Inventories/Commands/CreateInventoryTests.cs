using KuyumcuStokTakip.Application.Common.Exceptions;
using KuyumcuStokTakip.Application.Inventories.Commands.CreateInventory;
using KuyumcuStokTakip.Domain.Entities.Inventory;

namespace KuyumcuStokTakip.Application.FunctionalTests.Inventories.Commands;

using static Testing;

public class CreateInventoryTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireFields()
    {
        var command = new CreateInventoryCommand { Code = "", Name = "" };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateInventory()
    {
        var command = new CreateInventoryCommand
        {
            Code = "INV1",
            Name = "Inventory 1",
            AccountPartnerId = 1
        };

        var id = await SendAsync(command);
        var entity = await FindAsync<Inventory>(id);
        entity.Should().NotBeNull();
        entity!.Code.Should().Be(command.Code);
    }
}
