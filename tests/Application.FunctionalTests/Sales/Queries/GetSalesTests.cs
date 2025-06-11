using KuyumcuStokTakip.Application.Customers.Commands.CreateCustomer;
using KuyumcuStokTakip.Application.Sales.Commands.CreateSale;
using KuyumcuStokTakip.Application.Sales.Common;
using KuyumcuStokTakip.Application.Sales.Queries.GetSales;
using KuyumcuStokTakip.Application.StockTransactions.Commands.CreateStockTransaction;
using KuyumcuStokTakip.Application.StockTransactions.Queries.GetStockTransactions;
using KuyumcuStokTakip.Domain.Entities;
using KuyumcuStokTakip.Domain.Enums;

namespace KuyumcuStokTakip.Application.FunctionalTests.Sales.Queries;

using static Testing;

public class GetSalesTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnCreatedSale()
    {
        await RunAsDefaultUserAsync();

        var customerId = await SendAsync(new CreateCustomerCommand
        {
            FirstName = "John",
            LastName = "Doe"
        });

        var saleId = await SendAsync(new CreateSaleCommand
        {
            CustomerId = customerId,
            Items = [ new SaleItemDto
            {
                InventoryProductId = 1,
                Quantity = 2,
                UnitPrice = 100
            }]
        });

        var result = await SendAsync(new GetSalesQuery());

        var sale = result.Items.First(s => s.Id == saleId);
        sale.CustomerName.Should().Be("John Doe");
        sale.TotalAmount.Should().Be(200);
    }

    [Test]
    public async Task ShouldReturnProfit()
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

        var result = await SendAsync(new GetSalesQuery());
        var sale = result.Items.First(s => s.Id == saleId);

        sale.Items.First().Profit.Should().Be((120 - avg) * 2);
    }
}
