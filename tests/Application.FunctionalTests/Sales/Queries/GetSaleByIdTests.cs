using KuyumcuStokTakip.Application.Sales.Commands.CreateSale;
using KuyumcuStokTakip.Application.Sales.Queries.GetSaleById;

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
            Items = [ new CreateSaleCommand.SaleItemDto
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
}
