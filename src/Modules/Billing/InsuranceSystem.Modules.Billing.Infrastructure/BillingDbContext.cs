// File: InsuranceSystem.Modules.Billing.Infrastructure/BillingDbContext.cs
using InsuranceSystem.Modules.Billing.Domain.Entities;
using InsuranceSystem.Modules.Billing.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace InsuranceSystem.Modules.Billing.Infrastructure;

/// <summary>
/// DbContext for the Billing module.
/// </summary>
public class BillingDbContext(DbContextOptions<BillingDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Billing accounts in the system.
    /// </summary>
    public DbSet<BillingAccount> BillingAccounts => Set<BillingAccount>();

    /// <summary>
    /// Billing installments in the system.
    /// </summary>
    public DbSet<BillingInstallment> BillingInstallments => Set<BillingInstallment>();

    /// <summary>
    /// Invoices in the system.
    /// </summary>
    public DbSet<Invoice> Invoices => Set<Invoice>();

    /// <summary>
    /// Invoice installment mappings.
    /// </summary>
    public DbSet<InvoiceInstallment> InvoiceInstallments => Set<InvoiceInstallment>();

    /// <summary>
    /// Payments in the system.
    /// </summary>
    public DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all configurations from the Billing module
        modelBuilder.ApplyConfiguration(new BillingAccountConfiguration());
        modelBuilder.ApplyConfiguration(new BillingInstallmentConfiguration());
        modelBuilder.ApplyConfiguration(new InvoiceConfiguration());
        modelBuilder.ApplyConfiguration(new InvoiceInstallmentConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentConfiguration());

        // Set default schema for Oracle
        modelBuilder.HasDefaultSchema("BILLING");
    }
}
