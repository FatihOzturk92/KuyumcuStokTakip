using KuyumcuStokTakip.Application.Common.Exceptions;
using KuyumcuStokTakip.Application.Inventories.Commands.CreateInventory;
using KuyumcuStokTakip.Application.Inventories.Commands.DeleteInventory;
using KuyumcuStokTakip.Domain.Entities.Inventory;

namespace KuyumcuStokTakip.Application.FunctionalTests.Inventories.Commands;

using static Testing;

public class DeleteInventoryTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidId()
    {
        var command = new DeleteInventoryCommand(99);
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteInventory()
    {
        var id = await SendAsync(new CreateInventoryCommand
        {
            Code = "INV1",
            Name = "Inventory 1",
            AccountPartnerId = 1
        });

        await SendAsync(new DeleteInventoryCommand(id));
        var entity = await FindAsync<Inventory>(id);
        entity.Should().BeNull();
    }
}
