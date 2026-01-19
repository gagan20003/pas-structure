// File: InsuranceSystem.Modules.Contract.Infrastructure/ContractDbContext.cs
using InsuranceSystem.Modules.Contract.Domain.Entities;
using InsuranceSystem.Modules.Contract.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace InsuranceSystem.Modules.Contract.Infrastructure;

/// <summary>
/// DbContext for the Contract module.
/// </summary>
public class ContractDbContext(DbContextOptions<ContractDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Master contracts in the system.
    /// </summary>
    public DbSet<MasterContract> MasterContracts => Set<MasterContract>();

    /// <summary>
    /// Contracts in the system.
    /// </summary>
    public DbSet<Domain.Entities.Contract> Contracts => Set<Domain.Entities.Contract>();

    /// <summary>
    /// Endorsements in the system.
    /// </summary>
    public DbSet<Endorsement> Endorsements => Set<Endorsement>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all configurations from the Contract module
        modelBuilder.ApplyConfiguration(new MasterContractConfiguration());
        modelBuilder.ApplyConfiguration(new ContractConfiguration());
        modelBuilder.ApplyConfiguration(new EndorsementConfiguration());

        // Set default schema for Oracle
        modelBuilder.HasDefaultSchema("CONTRACT");
    }
}
