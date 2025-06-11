using System.Net.Http.Json;
using KuyumcuStokTakip.Application.Sales.Commands.CreateSale;
using KuyumcuStokTakip.Application.Sales.Common;
using KuyumcuStokTakip.Application.Sales.Queries.GetSaleById;
using KuyumcuStokTakip.Domain.Entities.Inventory;
using KuyumcuStokTakip.Domain.Entities.Sales;
using KuyumcuStokTakip.Application.Sales.Queries.GetSales;

namespace KuyumcuStokTakip.Application.FunctionalTests.Sales.Endpoints;

using static Testing;

public class SalesEndpointTests : BaseTestFixture
{
    [Test]
    public async Task ShouldCreateSale()
    {
        await RunAsDefaultUserAsync();
        var client = CreateClient();
        var before = await CountAsync<StockTransaction>();
        var command = new CreateSaleCommand
        {
            Items = [ new SaleItemDto
            {
                InventoryProductId = 1,
                Quantity = 1,
                UnitPrice = 100
            }]
        };

        var response = await client.PostAsJsonAsync("/api/Sales", command);
        response.EnsureSuccessStatusCode();
        var id = await response.Content.ReadFromJsonAsync<int>();

        var sale = await FindAsync<Sale>(id);
        sale.Should().NotBeNull();

        var after = await CountAsync<StockTransaction>();
        after.Should().Be(before + 1);
    }

    [Test]
    public async Task ShouldGetSale()
    {
        await RunAsDefaultUserAsync();
        var client = CreateClient();

        var saleId = await SendAsync(new CreateSaleCommand
        {
            Items = [ new SaleItemDto
            {
                InventoryProductId = 1,
                Quantity = 1,
                UnitPrice = 100
            }]
        });

        var response = await client.GetAsync($"/api/Sales/{saleId}");
        response.EnsureSuccessStatusCode();
        var sale = await response.Content.ReadFromJsonAsync<SaleDto>();

        sale.Should().NotBeNull();
        sale!.Id.Should().Be(saleId);
    }
}
