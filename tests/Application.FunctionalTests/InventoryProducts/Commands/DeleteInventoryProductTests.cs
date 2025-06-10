using KuyumcuStokTakip.Application.Common.Exceptions;
using KuyumcuStokTakip.Application.InventoryProducts.Commands.CreateInventoryProduct;
using KuyumcuStokTakip.Application.InventoryProducts.Commands.DeleteInventoryProduct;
using KuyumcuStokTakip.Domain.Entities.Inventory;

namespace KuyumcuStokTakip.Application.FunctionalTests.InventoryProducts.Commands;

using static Testing;

public class DeleteInventoryProductTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidId()
    {
        var command = new DeleteInventoryProductCommand(99);
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteInventoryProduct()
    {
        var id = await SendAsync(new CreateInventoryProductCommand
        {
            Code = "P1",
            Name = "Product 1",
            CategoryId = 1,
            InventoryId = 1,
            UnitId = 1
        });

        await SendAsync(new DeleteInventoryProductCommand(id));
        var entity = await FindAsync<InventoryProduct>(id);
        entity.Should().BeNull();
    }
}
