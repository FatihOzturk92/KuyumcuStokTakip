using System.Reflection;
using KuyumcuStokTakip.Application.Common.Interfaces;
using KuyumcuStokTakip.Domain.Entities;
using KuyumcuStokTakip.Domain.Entities.Account;
using KuyumcuStokTakip.Domain.Entities.Common;
using KuyumcuStokTakip.Domain.Entities.Finance;
using KuyumcuStokTakip.Domain.Entities.Inventory;
using KuyumcuStokTakip.Domain.Entities.Sales;
using KuyumcuStokTakip.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KuyumcuStokTakip.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<TodoList> TodoLists => Set<TodoList>();

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    // Account
    public DbSet<AccountPartner> AccountPartners => Set<AccountPartner>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Partner> Partners => Set<Partner>();

    // Common
    public DbSet<Branch> Branches => Set<Branch>();

    // Finance
    public DbSet<AccountTransaction> AccountTransactions => Set<AccountTransaction>();
    public DbSet<CashTransaction> CashTransactions => Set<CashTransaction>();
    public DbSet<Currency> Currencies => Set<Currency>();
    public DbSet<ExchangeRate> ExchangeRates => Set<ExchangeRate>();
    public DbSet<Expense> Expenses => Set<Expense>();
    public DbSet<PartnerAccountTransaction> PartnerAccountTransactions => Set<PartnerAccountTransaction>();

    // Inventory
    public DbSet<Inventory> Inventories => Set<Inventory>();
    public DbSet<InventoryProduct> InventoryProducts => Set<InventoryProduct>();
    public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
    public DbSet<ProductItem> ProductItems => Set<ProductItem>();
    public DbSet<Pricing> Pricings => Set<Pricing>();
    public DbSet<Purity> Purities => Set<Purity>();
    public DbSet<StockTransaction> StockTransactions => Set<StockTransaction>();
    public DbSet<StockUnit> StockUnits => Set<StockUnit>();

    // Sales
    public DbSet<Sale> Sales => Set<Sale>();
    public DbSet<SaleItem> SaleItems => Set<SaleItem>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
