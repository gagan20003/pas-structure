// File: InsuranceSystem.Modules.Billing.Infrastructure/Configurations/BillingAccountConfiguration.cs
using InsuranceSystem.Modules.Billing.Domain.Entities;
using InsuranceSystem.Modules.Billing.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsuranceSystem.Modules.Billing.Infrastructure.Configurations;

/// <summary>
/// EF Core configuration for BillingAccount entity.
/// </summary>
public class BillingAccountConfiguration : IEntityTypeConfiguration<BillingAccount>
{
    public void Configure(EntityTypeBuilder<BillingAccount> builder)
    {
        builder.ToTable("BILLING_ACCOUNTS");

        // Primary Key with Oracle IDENTITY
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasColumnName("ID")
            .UseIdentityColumn();

        // Properties
        builder.Property(e => e.MasterContractId)
            .HasColumnName("MASTER_CONTRACT_ID")
            .IsRequired();

        builder.Property(e => e.ContractId)
            .HasColumnName("CONTRACT_ID")
            .IsRequired();

        builder.Property(e => e.CurrencyId)
            .HasColumnName("CURRENCY_ID")
            .IsRequired();

        builder.Property(e => e.AccountNumber)
            .HasColumnName("ACCOUNT_NUMBER")
            .HasMaxLength(50)
            .IsRequired();

        // Enum to int conversion
        builder.Property(e => e.BillingAccountType)
            .HasColumnName("BILLING_ACCOUNT_TYPE")
            .HasConversion<int>()
            .IsRequired();

        builder.Property(e => e.BillingCycle)
            .HasColumnName("BILLING_CYCLE")
            .HasConversion<int>()
            .IsRequired();

        // Decimal precision for monetary values
        builder.Property(e => e.OutstandingAmount)
            .HasColumnName("OUTSTANDING_AMOUNT")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(e => e.TotalBilledAmount)
            .HasColumnName("TOTAL_BILLED_AMOUNT")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(e => e.Status)
            .HasColumnName("STATUS")
            .HasConversion<int>()
            .IsRequired();

        // Audit fields
        builder.Property(e => e.CreatedAt)
            .HasColumnName("CREATED_ON")
            .IsRequired();

        builder.Property(e => e.CreatedBy)
            .HasColumnName("CREATED_BY")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.UpdatedAt)
            .HasColumnName("MODIFIED_ON");

        builder.Property(e => e.UpdatedBy)
            .HasColumnName("MODIFIED_BY")
            .HasMaxLength(100);

        // Indexes
        builder.HasIndex(e => e.AccountNumber)
            .IsUnique()
            .HasDatabaseName("IX_BILLING_ACCOUNTS_ACCOUNT_NUMBER");

        builder.HasIndex(e => e.ContractId)
            .HasDatabaseName("IX_BILLING_ACCOUNTS_CONTRACT_ID");

        builder.HasIndex(e => e.MasterContractId)
            .HasDatabaseName("IX_BILLING_ACCOUNTS_MASTER_CONTRACT_ID");
    }
}
