using KuyumcuStokTakip.Application.Common.Exceptions;
using KuyumcuStokTakip.Application.Sales.Common;
using KuyumcuStokTakip.Application.Sales.Commands.CreateSale;
using KuyumcuStokTakip.Application.Customers.Commands.CreateCustomer;
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
            Items = [ new SaleItemDto { Quantity = 0, UnitPrice = -1 } ]
        };
        await FluentActions.Invoking(() => SendAsync(command))
            .Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateSaleAndStockTransactions()
    {
        var before = await CountAsync<StockTransaction>();

        var customerId = await SendAsync(new CreateCustomerCommand
        {
            FirstName = "Jane",
            LastName = "Doe"
        });

        var command = new CreateSaleCommand
        {
            CustomerId = customerId,
            Items = [ new SaleItemDto
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

        var transaction = await FindAsync<StockTransaction>(after);
        transaction.Should().NotBeNull();
        transaction!.CustomerId.Should().Be(customerId);
    }
}
