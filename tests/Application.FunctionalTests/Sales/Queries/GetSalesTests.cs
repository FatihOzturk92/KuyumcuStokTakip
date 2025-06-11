using KuyumcuStokTakip.Application.Customers.Commands.CreateCustomer;
using KuyumcuStokTakip.Application.Sales.Commands.CreateSale;
using KuyumcuStokTakip.Application.Sales.Common;
using KuyumcuStokTakip.Application.Sales.Queries.GetSales;
using KuyumcuStokTakip.Application.StockTransactions.Commands.CreateStockTransaction;
using KuyumcuStokTakip.Application.StockTransactions.Queries.GetStockTransactions;
using KuyumcuStokTakip.Application.InventoryProducts.Commands.CreateInventoryProduct;
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

    [Test]
    public async Task ShouldFilterByDate()
    {
        await RunAsDefaultUserAsync();

        var oldSaleId = await SendAsync(new CreateSaleCommand
        {
            SaleDate = DateTime.UtcNow.AddDays(-5),
            Items = [ new SaleItemDto { InventoryProductId = 1, Quantity = 1, UnitPrice = 50 } ]
        });

        var recentSaleId = await SendAsync(new CreateSaleCommand
        {
            SaleDate = DateTime.UtcNow,
            Items = [ new SaleItemDto { InventoryProductId = 1, Quantity = 1, UnitPrice = 60 } ]
        });

        var result = await SendAsync(new GetSalesQuery
        {
            StartDate = DateTime.UtcNow.AddDays(-1),
            EndDate = DateTime.UtcNow.AddDays(1)
        });

        result.Items.Should().ContainSingle(s => s.Id == recentSaleId);
        result.Items.Should().NotContain(s => s.Id == oldSaleId);
    }

    [Test]
    public async Task ShouldFilterByCustomerName()
    {
        await RunAsDefaultUserAsync();

        var johnId = await SendAsync(new CreateCustomerCommand { FirstName = "John", LastName = "Smith" });
        var janeId = await SendAsync(new CreateCustomerCommand { FirstName = "Jane", LastName = "Doe" });

        var johnSale = await SendAsync(new CreateSaleCommand
        {
            CustomerId = johnId,
            Items = [ new SaleItemDto { InventoryProductId = 1, Quantity = 1, UnitPrice = 10 } ]
        });

        var janeSale = await SendAsync(new CreateSaleCommand
        {
            CustomerId = janeId,
            Items = [ new SaleItemDto { InventoryProductId = 1, Quantity = 1, UnitPrice = 20 } ]
        });

        var result = await SendAsync(new GetSalesQuery { CustomerName = "Jane" });

        result.Items.Should().ContainSingle(s => s.Id == janeSale);
        result.Items.Should().NotContain(s => s.Id == johnSale);
    }

    [Test]
    public async Task ShouldFilterByCategory()
    {
        await RunAsDefaultUserAsync();

        var product2Id = await SendAsync(new CreateInventoryProductCommand
        {
            Code = "P2",
            Name = "Prod2",
            CategoryId = 2,
            InventoryId = 1,
            UnitId = 1,
            TotalWeight = 0
        });

        var sale1 = await SendAsync(new CreateSaleCommand
        {
            Items = [ new SaleItemDto { InventoryProductId = 1, Quantity = 1, UnitPrice = 10 } ]
        });

        var sale2 = await SendAsync(new CreateSaleCommand
        {
            Items = [ new SaleItemDto { InventoryProductId = product2Id, Quantity = 1, UnitPrice = 20 } ]
        });

        var result = await SendAsync(new GetSalesQuery { CategoryId = 2 });

        result.Items.Should().ContainSingle(s => s.Id == sale2);
        result.Items.Should().NotContain(s => s.Id == sale1);
    }
}
