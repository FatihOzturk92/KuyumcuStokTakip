using KuyumcuStokTakip.Application.Common.Exceptions;
using KuyumcuStokTakip.Application.Sales.Commands.ReturnSale;
using KuyumcuStokTakip.Application.Sales.Common;
using KuyumcuStokTakip.Domain.Entities;
using KuyumcuStokTakip.Domain.Entities.Inventory;
using KuyumcuStokTakip.Domain.Enums;

namespace KuyumcuStokTakip.Application.FunctionalTests.Sales.Commands;

using static Testing;

public class ReturnSaleTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireFields()
    {
        await RunAsDefaultUserAsync();
        var command = new ReturnSaleCommand();
        await FluentActions.Invoking(() => SendAsync(command))
            .Should().ThrowAsync<ValidationException>();

        command = new ReturnSaleCommand
        {
            Items = [ new SaleItemDto { Quantity = 0, UnitPrice = -1 } ]
        };
        await FluentActions.Invoking(() => SendAsync(command))
            .Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateReturnTransactions()
    {
        await RunAsDefaultUserAsync();
        var before = await CountAsync<StockTransaction>();

        var command = new ReturnSaleCommand
        {
            Items = [ new SaleItemDto
            {
                InventoryProductId = 1,
                Quantity = 1,
                UnitPrice = 100
            }]
        };

        await SendAsync(command);

        var after = await CountAsync<StockTransaction>();
        after.Should().Be(before + 1);
        var transaction = await FindAsync<StockTransaction>(after);
        transaction!.TransactionType.Should().Be(TransactionType.Return);
        transaction.Type.Should().Be(EStockTransactionType.In);
    }
}
