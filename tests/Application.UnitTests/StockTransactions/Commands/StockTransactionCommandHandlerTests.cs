using KuyumcuStokTakip.Application.StockTransactions.Commands.CreateStockTransaction;
using KuyumcuStokTakip.Application.StockTransactions.Commands.DeleteStockTransaction;
using KuyumcuStokTakip.Application.StockTransactions.Commands.UpdateStockTransaction;
using KuyumcuStokTakip.Domain.Entities.Inventory;
using KuyumcuStokTakip.Domain.Entities;
using KuyumcuStokTakip.Domain.Enums;
using KuyumcuStokTakip.Application.Common.Interfaces;
using Moq;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;

namespace KuyumcuStokTakip.Application.UnitTests.StockTransactions.Commands;

public class StockTransactionCommandHandlerTests
{
    private Mock<IApplicationDbContext> _context = null!;
    private Mock<DbSet<StockTransaction>> _dbSet = null!;

    [SetUp]
    public void Setup()
    {
        _context = new Mock<IApplicationDbContext>();
        _dbSet = new Mock<DbSet<StockTransaction>>();
        _context.SetupGet(c => c.StockTransactions).Returns(_dbSet.Object);
        _context.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
    }

    [Test]
    public async Task CreateHandlerAddsEntity()
    {
        var handler = new CreateStockTransactionCommandHandler(_context.Object);
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

        await handler.Handle(command, CancellationToken.None);

        _dbSet.Verify(d => d.Add(It.IsAny<StockTransaction>()), Times.Once);
        _context.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task UpdateHandlerUpdatesEntity()
    {
        var entity = new StockTransaction { Id = 1 };
        var dbSet = new Mock<DbSet<StockTransaction>>();
        dbSet.Setup(d => d.FindAsync(new object[] { 1 }, It.IsAny<CancellationToken>())).ReturnsAsync(entity);
        _context.SetupGet(c => c.StockTransactions).Returns(dbSet.Object);

        var handler = new UpdateStockTransactionCommandHandler(_context.Object);
        var command = new UpdateStockTransactionCommand
        {
            Id = 1,
            InventoryProductId = 1,
            ProductId = 1,
            Quantity = 2,
            Weight = 2,
            UnitPriceType = EUnitPriceType.Milyem,
            Type = EStockTransactionType.Out,
            TransactionType = TransactionType.ManualAdjustment
        };

        await handler.Handle(command, CancellationToken.None);

        dbSet.Verify(d => d.FindAsync(new object[] { 1 }, It.IsAny<CancellationToken>()), Times.Once);
        _context.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.That(entity.Quantity, Is.EqualTo(2));
        Assert.That(entity.Type, Is.EqualTo(EStockTransactionType.Out));
    }

    [Test]
    public async Task DeleteHandlerRemovesEntity()
    {
        var entity = new StockTransaction { Id = 1 };
        var dbSet = new Mock<DbSet<StockTransaction>>();
        dbSet.Setup(d => d.FindAsync(new object[] { 1 }, It.IsAny<CancellationToken>())).ReturnsAsync(entity);
        _context.SetupGet(c => c.StockTransactions).Returns(dbSet.Object);

        var handler = new DeleteStockTransactionCommandHandler(_context.Object);
        var command = new DeleteStockTransactionCommand(1);

        await handler.Handle(command, CancellationToken.None);

        dbSet.Verify(d => d.FindAsync(new object[] { 1 }, It.IsAny<CancellationToken>()), Times.Once);
        _context.Verify(c => c.StockTransactions.Remove(entity), Times.Once);
        _context.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
