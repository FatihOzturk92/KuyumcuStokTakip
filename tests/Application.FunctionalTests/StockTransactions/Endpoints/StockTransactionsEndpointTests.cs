using System.Net.Http.Json;
using KuyumcuStokTakip.Application.StockTransactions.Commands.CreateStockTransaction;
using KuyumcuStokTakip.Application.StockTransactions.Commands.UpdateStockTransaction;
using KuyumcuStokTakip.Domain.Entities.Inventory;

namespace KuyumcuStokTakip.Application.FunctionalTests.StockTransactions.Endpoints;

using static Testing;

public class StockTransactionsEndpointTests : BaseTestFixture
{
    [Test]
    public async Task ShouldCreateStockTransaction()
    {
        await RunAsDefaultUserAsync();
        var client = CreateClient();
        var command = new CreateStockTransactionCommand
        {
            InventoryProductId = 1,
            Quantity = 1,
            Weight = 1,
            UnitPriceType = EUnitPriceType.Milyem,
            Type = EStockTransactionType.In
        };

        var response = await client.PostAsJsonAsync("/api/StockTransactions", command);
        response.EnsureSuccessStatusCode();
        var id = await response.Content.ReadFromJsonAsync<int>();

        var entity = await FindAsync<StockTransaction>(id);
        entity.Should().NotBeNull();
        entity!.InventoryProductId.Should().Be(command.InventoryProductId);
    }

    [Test]
    public async Task ShouldUpdateStockTransaction()
    {
        await RunAsDefaultUserAsync();
        var client = CreateClient();
        var id = await SendAsync(new CreateStockTransactionCommand
        {
            InventoryProductId = 1,
            Quantity = 1,
            Weight = 1,
            UnitPriceType = EUnitPriceType.Milyem,
            Type = EStockTransactionType.In
        });

        var update = new UpdateStockTransactionCommand
        {
            Id = id,
            InventoryProductId = 1,
            Quantity = 2,
            Weight = 2,
            UnitPriceType = EUnitPriceType.Milyem,
            Type = EStockTransactionType.Out
        };

        var response = await client.PutAsJsonAsync($"/api/StockTransactions/{id}", update);
        response.EnsureSuccessStatusCode();

        var entity = await FindAsync<StockTransaction>(id);
        entity!.Quantity.Should().Be(update.Quantity);
        entity.Type.Should().Be(update.Type);
    }

    [Test]
    public async Task ShouldDeleteStockTransaction()
    {
        await RunAsDefaultUserAsync();
        var client = CreateClient();
        var id = await SendAsync(new CreateStockTransactionCommand
        {
            InventoryProductId = 1,
            Quantity = 1,
            Weight = 1,
            UnitPriceType = EUnitPriceType.Milyem,
            Type = EStockTransactionType.In
        });

        var response = await client.DeleteAsync($"/api/StockTransactions/{id}");
        response.EnsureSuccessStatusCode();

        var entity = await FindAsync<StockTransaction>(id);
        entity.Should().BeNull();
    }
}
