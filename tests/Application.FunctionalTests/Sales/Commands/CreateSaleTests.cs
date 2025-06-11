using KuyumcuStokTakip.Application.Common.Exceptions;
using KuyumcuStokTakip.Application.Sales.Commands.CreateSale;
using KuyumcuStokTakip.Domain.Entities.Inventory;
using KuyumcuStokTakip.Domain.Entities.Sales;

namespace KuyumcuStokTakip.Application.FunctionalTests.Sales.Commands;

using static Testing;

public class CreateSaleTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireFields()
    {
        var command = new CreateSaleCommand();
        await FluentActions.Invoking(() => SendAsync(command))
            .Should().ThrowAsync<ValidationException>();

        command = new CreateSaleCommand
        {
            Items = [ new CreateSaleCommand.SaleItemDto { Quantity = 0, UnitPrice = -1 } ]
        };
        await FluentActions.Invoking(() => SendAsync(command))
            .Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateSaleAndStockTransactions()
    {
        var before = await CountAsync<StockTransaction>();

        var command = new CreateSaleCommand
        {
            Items = [ new CreateSaleCommand.SaleItemDto
            {
                InventoryProductId = 1,
                Quantity = 1,
                UnitPrice = 100
            }]
        };

        var id = await SendAsync(command);

        var sale = await FindAsync<Sale>(id);
        sale.Should().NotBeNull();

        var after = await CountAsync<StockTransaction>();
        after.Should().Be(before + 1);
    }
}
