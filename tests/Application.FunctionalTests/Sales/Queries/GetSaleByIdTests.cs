using KuyumcuStokTakip.Application.Sales.Commands.CreateSale;
using KuyumcuStokTakip.Application.Sales.Common;
using KuyumcuStokTakip.Application.Sales.Queries.GetSaleById;
using KuyumcuStokTakip.Application.StockTransactions.Commands.CreateStockTransaction;
using KuyumcuStokTakip.Application.StockTransactions.Queries.GetStockTransactions;
using KuyumcuStokTakip.Domain.Entities;
using KuyumcuStokTakip.Domain.Enums;

namespace KuyumcuStokTakip.Application.FunctionalTests.Sales.Queries;

using static Testing;

public class GetSaleByIdTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnSale()
    {
        await RunAsDefaultUserAsync();

        var saleId = await SendAsync(new CreateSaleCommand
        {
            Items = [ new SaleItemDto
            {
                InventoryProductId = 1,
                Quantity = 1,
                UnitPrice = 100
            }]
        });

        var query = new GetSaleByIdQuery(saleId);
        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result.Id.Should().Be(saleId);
    }

    [Test]
    public async Task ShouldCalculateProfit()
    {
        await RunAsDefaultUserAsync();

        await SendAsync(new CreateStockTransactionCommand
        {
            InventoryProductId = 1,
            ProductId = 1,
            Quantity = 1,
            Weight = 1,
            UnitPrice = 50,
            UnitPriceType = EUnitPriceType.TL,
            Type = EStockTransactionType.In,
            TransactionType = TransactionType.Purchase
        });

        await SendAsync(new CreateStockTransactionCommand
        {
            InventoryProductId = 1,
            ProductId = 1,
            Quantity = 1,
            Weight = 1,
            UnitPrice = 100,
            UnitPriceType = EUnitPriceType.TL,
            Type = EStockTransactionType.In,
            TransactionType = TransactionType.Purchase
        });

        var saleId = await SendAsync(new CreateSaleCommand
        {
            Items = [ new SaleItemDto
            {
                InventoryProductId = 1,
                Quantity = 2,
                UnitPrice = 120
            }]
        });

        var transactions = await SendAsync(new GetStockTransactionsQuery { PageSize = 50 });
        var avg = transactions.Items.Where(t => t.InventoryProductId == 1 && t.Type == EStockTransactionType.In).Average(t => t.UnitPrice);

        var result = await SendAsync(new GetSaleByIdQuery(saleId));
        var profit = result.Items.First().Profit;

        profit.Should().Be((120 - avg) * 2);
    }
}
