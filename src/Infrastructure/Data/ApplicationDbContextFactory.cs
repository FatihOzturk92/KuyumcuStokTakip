using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace KuyumcuStokTakip.Infrastructure.Data;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        // Use connection string from environment variable or default to local SQL Server
        var connectionString = Environment.GetEnvironmentVariable("KuyumcuStokTakipDb")
            ?? "Server=localhost,1433;Database=KuyumcuStokTakipDb;User Id=sa;Password=123A.a321;TrustServerCertificate=True;";
        optionsBuilder.UseSqlServer(connectionString);

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
