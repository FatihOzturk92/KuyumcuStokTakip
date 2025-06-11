using KuyumcuStokTakip.Application.Customers.Commands.CreateCustomer;
using KuyumcuStokTakip.Application.Sales.Commands.CreateSale;
using KuyumcuStokTakip.Application.Sales.Common;
using KuyumcuStokTakip.Application.Sales.Queries.GetSales;

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
}
