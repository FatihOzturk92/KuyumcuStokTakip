using KuyumcuStokTakip.Application.Common.Exceptions;
using KuyumcuStokTakip.Application.InventoryProducts.Commands.CreateInventoryProduct;
using KuyumcuStokTakip.Domain.Entities.Inventory;

namespace KuyumcuStokTakip.Application.FunctionalTests.InventoryProducts.Commands;

using static Testing;

public class CreateInventoryProductTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireFields()
    {
        var command = new CreateInventoryProductCommand { Code = "", Name = "" };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateInventoryProduct()
    {
        var command = new CreateInventoryProductCommand
        {
            Code = "P1",
            Name = "Product 1",
            CategoryId = 1,
            InventoryId = 1,
            UnitId = 1,
            TotalWeight = 1
        };

        var id = await SendAsync(command);
        var entity = await FindAsync<InventoryProduct>(id);
        entity.Should().NotBeNull();
        entity!.Code.Should().Be(command.Code);
    }
}
