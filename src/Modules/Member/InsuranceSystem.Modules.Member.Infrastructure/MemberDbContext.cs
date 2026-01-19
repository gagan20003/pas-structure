// File: InsuranceSystem.Modules.Member.Infrastructure/MemberDbContext.cs
using InsuranceSystem.Modules.Member.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace InsuranceSystem.Modules.Member.Infrastructure;

/// <summary>
/// DbContext for the Member module.
/// </summary>
public class MemberDbContext(DbContextOptions<MemberDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Members in the system.
    /// </summary>
    public DbSet<Domain.Entities.Member> Members => Set<Domain.Entities.Member>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all configurations from the Member module
        modelBuilder.ApplyConfiguration(new MemberConfiguration());

        // Set default schema for Oracle
        modelBuilder.HasDefaultSchema("MEMBER");
    }
}
