using KuyumcuStokTakip.Domain.Entities;
using KuyumcuStokTakip.Domain.Entities.Account;
using KuyumcuStokTakip.Domain.Entities.Common;
using KuyumcuStokTakip.Domain.Entities.Finance;
using KuyumcuStokTakip.Domain.Entities.Inventory;
using KuyumcuStokTakip.Domain.Entities.Sales;

namespace KuyumcuStokTakip.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    // Account
    DbSet<AccountPartner> AccountPartners { get; }
    DbSet<Customer> Customers { get; }
    DbSet<Partner> Partners { get; }

    // Common
    DbSet<Branch> Branches { get; }

    // Finance
    DbSet<AccountTransaction> AccountTransactions { get; }
    DbSet<CashTransaction> CashTransactions { get; }
    DbSet<Currency> Currencies { get; }
    DbSet<ExchangeRate> ExchangeRates { get; }
    DbSet<Expense> Expenses { get; }
    DbSet<PartnerAccountTransaction> PartnerAccountTransactions { get; }

    // Inventory
    DbSet<Inventory> Inventories { get; }
    DbSet<InventoryProduct> InventoryProducts { get; }
    DbSet<ProductCategory> ProductCategories { get; }
    DbSet<Product> Products { get; }
    DbSet<ProductItem> ProductItems { get; }
    DbSet<Pricing> Pricings { get; }
    DbSet<Purity> Purities { get; }
    DbSet<StockTransaction> StockTransactions { get; }
    DbSet<StockUnit> StockUnits { get; }

    // Sales
    DbSet<Sale> Sales { get; }
    DbSet<SaleItem> SaleItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
