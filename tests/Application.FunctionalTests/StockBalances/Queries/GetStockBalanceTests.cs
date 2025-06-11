using KuyumcuStokTakip.Application.StockTransactions.Commands.CreateStockTransaction;
using KuyumcuStokTakip.Application.InventoryProducts.Commands.CreateInventoryProduct;
using KuyumcuStokTakip.Application.StockBalances.Queries.GetStockBalance;
using KuyumcuStokTakip.Domain.Entities;
using KuyumcuStokTakip.Domain.Enums;
using System.Linq;

namespace KuyumcuStokTakip.Application.FunctionalTests.StockBalances.Queries;

using static Testing;

public class GetStockBalanceTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnCorrectSums()
    {
        await RunAsDefaultUserAsync();

        await SendAsync(new CreateStockTransactionCommand
        {
            InventoryProductId = 1,
            ProductId = 1,
            Quantity = 5,
            Weight = 5,
            UnitPriceType = EUnitPriceType.Milyem,
            Type = EStockTransactionType.In,
            TransactionType = TransactionType.Purchase
        });

        await SendAsync(new CreateStockTransactionCommand
        {
            InventoryProductId = 1,
            ProductId = 1,
            Quantity = 2,
            Weight = 2,
            UnitPriceType = EUnitPriceType.Milyem,
            Type = EStockTransactionType.Out,
            TransactionType = TransactionType.ManualAdjustment
        });

        var result = await SendAsync(new GetStockBalanceQuery());
        var balance = result.First(x => x.ProductId == 1);

        balance.TotalIn.Should().Be(5);
        balance.TotalOut.Should().Be(2);
        balance.Net.Should().Be(3);
    }

    [Test]
    public async Task ShouldFilterByProductName()
    {
        await RunAsDefaultUserAsync();

        var productId = await SendAsync(new CreateInventoryProductCommand
        {
            Code = "P2",
            Name = "Silver Ring",
            CategoryId = 1,
            InventoryId = 1,
            UnitId = 1,
            TotalWeight = 1
        });

        await SendAsync(new CreateStockTransactionCommand
        {
            InventoryProductId = productId,
            ProductId = productId,
            Quantity = 4,
            Weight = 4,
            UnitPriceType = EUnitPriceType.Milyem,
            Type = EStockTransactionType.In,
            TransactionType = TransactionType.Purchase
        });

        await SendAsync(new CreateStockTransactionCommand
        {
            InventoryProductId = 1,
            ProductId = 1,
            Quantity = 1,
            Weight = 1,
            UnitPriceType = EUnitPriceType.Milyem,
            Type = EStockTransactionType.In,
            TransactionType = TransactionType.Purchase
        });

        var result = await SendAsync(new GetStockBalanceQuery { SearchTerm = "Silver" });

        result.Should().ContainSingle();
        result.First().ProductId.Should().Be(productId);
    }
}

