using System.Net.Http.Json;
using KuyumcuStokTakip.Application.StockTransactions.Commands.CreateStockTransaction;
using KuyumcuStokTakip.Application.StockTransactions.Commands.UpdateStockTransaction;
using KuyumcuStokTakip.Domain.Entities.Inventory;
using KuyumcuStokTakip.Domain.Entities;
using KuyumcuStokTakip.Domain.Enums;

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
            ProductId = 1,
            Quantity = 1,
            Weight = 1,
            UnitPriceType = EUnitPriceType.Milyem,
            Type = EStockTransactionType.In,
            TransactionType = TransactionType.Purchase
        };

        var response = await client.PostAsJsonAsync("/api/StockTransactions", command);
        response.EnsureSuccessStatusCode();
        var id = await response.Content.ReadFromJsonAsync<int>();

        var entity = await FindAsync<StockTransaction>(id);
        entity.Should().NotBeNull();
        entity!.InventoryProductId.Should().Be(command.InventoryProductId);
    }

    [Test]
    public async Task ShouldCreateWastageReturnAndExchange()
    {
        await RunAsDefaultUserAsync();
        var client = CreateClient();

        var wastage = new CreateStockTransactionCommand
        {
            InventoryProductId = 1,
            ProductId = 1,
            Quantity = 1,
            Weight = 1,
            UnitPriceType = EUnitPriceType.Milyem,
            Type = EStockTransactionType.Out,
            TransactionType = TransactionType.Wastage
        };
        var response = await client.PostAsJsonAsync("/api/StockTransactions", wastage);
        response.EnsureSuccessStatusCode();
        var id = await response.Content.ReadFromJsonAsync<int>();
        var entity = await FindAsync<StockTransaction>(id);
        entity!.TransactionType.Should().Be(TransactionType.Wastage);

        var @return = new CreateStockTransactionCommand
        {
            InventoryProductId = 1,
            ProductId = 1,
            Quantity = 1,
            Weight = 1,
            UnitPriceType = EUnitPriceType.Milyem,
            Type = EStockTransactionType.In,
            TransactionType = TransactionType.Return,
            CustomerId = 1
        };
        response = await client.PostAsJsonAsync("/api/StockTransactions", @return);
        response.EnsureSuccessStatusCode();
        id = await response.Content.ReadFromJsonAsync<int>();
        entity = await FindAsync<StockTransaction>(id);
        entity!.TransactionType.Should().Be(TransactionType.Return);

        var before = await CountAsync<StockTransaction>();
        var exchange = new CreateStockTransactionCommand
        {
            InventoryProductId = 1,
            ProductId = 1,
            Quantity = 1,
            Weight = 1,
            UnitPriceType = EUnitPriceType.Milyem,
            Type = EStockTransactionType.Out,
            TransactionType = TransactionType.Exchange
        };
        response = await client.PostAsJsonAsync("/api/StockTransactions", exchange);
        response.EnsureSuccessStatusCode();
        id = await response.Content.ReadFromJsonAsync<int>();
        entity = await FindAsync<StockTransaction>(id);
        entity!.TransactionType.Should().Be(TransactionType.Exchange);
        var after = await CountAsync<StockTransaction>();
        after.Should().BeGreaterThan(before);
    }

    [Test]
    public async Task ShouldUpdateStockTransaction()
    {
        await RunAsDefaultUserAsync();
        var client = CreateClient();
        var id = await SendAsync(new CreateStockTransactionCommand
        {
            InventoryProductId = 1,
            ProductId = 1,
            Quantity = 1,
            Weight = 1,
            UnitPriceType = EUnitPriceType.Milyem,
            Type = EStockTransactionType.In,
            TransactionType = TransactionType.Purchase
        });

        var update = new UpdateStockTransactionCommand
        {
            Id = id,
            InventoryProductId = 1,
            ProductId = 1,
            Quantity = 2,
            Weight = 2,
            UnitPriceType = EUnitPriceType.Milyem,
            Type = EStockTransactionType.Out,
            TransactionType = TransactionType.ManualAdjustment
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
            ProductId = 1,
            Quantity = 1,
            Weight = 1,
            UnitPriceType = EUnitPriceType.Milyem,
            Type = EStockTransactionType.In,
            TransactionType = TransactionType.Purchase
        });

        var response = await client.DeleteAsync($"/api/StockTransactions/{id}");
        response.EnsureSuccessStatusCode();

        var entity = await FindAsync<StockTransaction>(id);
        entity.Should().BeNull();
    }
}
