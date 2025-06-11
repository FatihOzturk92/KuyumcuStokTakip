using KuyumcuStokTakip.Domain.Constants;
using KuyumcuStokTakip.Domain.Entities;
using KuyumcuStokTakip.Infrastructure.Identity;
using KuyumcuStokTakip.Domain.Entities.Account;
using KuyumcuStokTakip.Domain.Entities.Inventory;
using KuyumcuStokTakip.Domain.Entities.Sales;
using KuyumcuStokTakip.Domain.Enums;
using Bogus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KuyumcuStokTakip.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static void AddAsyncSeeding(this DbContextOptionsBuilder builder, IServiceProvider serviceProvider)
    {
        builder.UseAsyncSeeding(async (context, _, ct) =>
        {
            var initialiser = serviceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

            await initialiser.SeedAsync();
        });
    }

    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var administratorRole = new IdentityRole(Roles.Administrator);

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        // Default users
        var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!");
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new [] { administratorRole.Name });
            }
        }

        // Default data
        // Seed, if necessary
        if (!_context.TodoLists.Any())
        {
            _context.TodoLists.Add(new TodoList
            {
                Title = "Todo List",
                Items =
                {
                    new TodoItem { Title = "Make a todo list 📃" },
                    new TodoItem { Title = "Check off the first item ✅" },
                    new TodoItem { Title = "Realise you've already done two things on the list! 🤯"},
                    new TodoItem { Title = "Reward yourself with a nice, long nap 🏆" },
                }
            });

            await _context.SaveChangesAsync();
        }

        // Seed partners
        if (!_context.Partners.Any())
        {
            var faker = new Bogus.Faker("tr");
            _context.Partners.AddRange(
                new Partner
                {
                    Name = faker.Company.CompanyName(),
                    Type = PartnerType.Wholesaler.ToString(),
                    PartnerPhone = faker.Phone.PhoneNumber(),
                    PartnerEmail = faker.Internet.Email(),
                    PartnerAddress = faker.Address.FullAddress(),
                    Note = faker.Lorem.Sentence()
                },
                new Partner
                {
                    Name = faker.Company.CompanyName(),
                    Type = PartnerType.Workshop.ToString(),
                    PartnerPhone = faker.Phone.PhoneNumber(),
                    PartnerEmail = faker.Internet.Email(),
                    PartnerAddress = faker.Address.FullAddress(),
                    Note = faker.Lorem.Sentence()
                });

            await _context.SaveChangesAsync();
        }

        // Seed inventories
        if (!_context.Inventories.Any())
        {
            var partner = _context.Partners.First();
            _context.Inventories.Add(new Inventory
            {
                Code = "INV001",
                Name = "Main Inventory",
                Description = "Default inventory",
                AccountPartnerId = partner.Id
            });

            await _context.SaveChangesAsync();
        }

        // Seed product categories
        if (!_context.ProductCategories.Any())
        {
            _context.ProductCategories.AddRange(
                new ProductCategory { Name = "Altın" },
                new ProductCategory { Name = "Gümüş" });

            await _context.SaveChangesAsync();
        }

        // Seed stock units
        if (!_context.StockUnits.Any())
        {
            _context.StockUnits.AddRange(
                new StockUnit { Name = "Gram", Symbol = "g" },
                new StockUnit { Name = "Adet", Symbol = "adet" });

            await _context.SaveChangesAsync();
        }

        // Seed inventory products
        if (!_context.InventoryProducts.Any())
        {
            var inventory = _context.Inventories.First();
            var category = _context.ProductCategories.First();
            var unit = _context.StockUnits.First();

            _context.InventoryProducts.Add(new InventoryProduct
            {
                Code = "PRD001",
                Name = "Altın Bilezik",
                InventoryId = inventory.Id,
                CategoryId = category.Id,
                UnitId = unit.Id,
                TotalWeight = 0,
                TotalPieceCount = 0,
                TotalLaborCost = 0,
                TotalMaterialCost = 0
            });

            await _context.SaveChangesAsync();
        }

        // Seed stock transactions
        if (!_context.StockTransactions.Any())
        {
            var faker = new Bogus.Faker("tr");
            var product = _context.InventoryProducts.First();

            _context.StockTransactions.Add(new StockTransaction
            {
                InventoryProductId = product.Id,
                ProductId = product.Id,
                TransactionDate = faker.Date.Recent(),
                Quantity = faker.Random.Decimal(1, 10),
                UnitPrice = faker.Random.Decimal(1000, 2000),
                Weight = faker.Random.Decimal(1, 100),
                PureGram = faker.Random.Decimal(1, 100),
                PureUnitPrice = faker.Random.Decimal(1000, 2000),
                LaborUnitPrice = faker.Random.Decimal(50, 100),
                UnitPriceType = EUnitPriceType.TL,
                TransactionType = TransactionType.Purchase,
                Type = EStockTransactionType.In,
                TotalCost = faker.Random.Decimal(1000, 5000)
            });

            await _context.SaveChangesAsync();
        }

        // Seed sales
        if (!_context.Sales.Any())
        {
            var product = _context.InventoryProducts.First();

            var sale = new Sale
            {
                SaleDate = DateTime.UtcNow,
                PaymentMethod = EPaymentType.Cash,
                Currency = "TL",
                Description = "Sample sale"
            };

            var saleItem = new SaleItem
            {
                InventoryProductId = product.Id,
                Quantity = 1,
                UnitPrice = 1200
            };

            sale.Items.Add(saleItem);

            var saleTransaction = new StockTransaction
            {
                InventoryProductId = product.Id,
                ProductId = product.Id,
                TransactionDate = sale.SaleDate,
                Quantity = saleItem.Quantity,
                UnitPrice = saleItem.UnitPrice,
                TransactionType = TransactionType.Sale,
                Type = EStockTransactionType.Out
            };

            _context.StockTransactions.Add(saleTransaction);

            _context.Sales.Add(sale);

            await _context.SaveChangesAsync();
        }
    }
}
