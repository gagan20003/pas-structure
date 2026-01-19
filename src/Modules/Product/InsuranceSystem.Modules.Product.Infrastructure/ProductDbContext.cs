// File: InsuranceSystem.Modules.Product.Infrastructure/ProductDbContext.cs
using InsuranceSystem.Modules.Product.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace InsuranceSystem.Modules.Product.Infrastructure;

/// <summary>
/// DbContext for the Product module.
/// </summary>
public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Products in the system.
    /// </summary>
    public DbSet<Domain.Entities.Product> Products => Set<Domain.Entities.Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all configurations from the Product module
        modelBuilder.ApplyConfiguration(new ProductConfiguration());

        // Set default schema for Oracle
        modelBuilder.HasDefaultSchema("PRODUCT");
    }
}
