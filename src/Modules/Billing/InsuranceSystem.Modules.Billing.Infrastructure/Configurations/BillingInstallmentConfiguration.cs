// File: InsuranceSystem.Modules.Billing.Infrastructure/Configurations/BillingInstallmentConfiguration.cs
using InsuranceSystem.Modules.Billing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsuranceSystem.Modules.Billing.Infrastructure.Configurations;

/// <summary>
/// EF Core configuration for BillingInstallment entity.
/// </summary>
public class BillingInstallmentConfiguration : IEntityTypeConfiguration<BillingInstallment>
{
    public void Configure(EntityTypeBuilder<BillingInstallment> builder)
    {
        builder.ToTable("BILLING_INSTALLMENTS");

        // Primary Key with Oracle IDENTITY
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasColumnName("ID")
            .UseIdentityColumn();

        // Foreign Keys
        builder.Property(e => e.BillingAccountId)
            .HasColumnName("BILLING_ACCOUNT_ID")
            .IsRequired();

        builder.Property(e => e.EndorsementId)
            .HasColumnName("ENDORSEMENT_ID");

        builder.Property(e => e.ContractId)
            .HasColumnName("CONTRACT_ID")
            .IsRequired();

        builder.Property(e => e.MemberId)
            .HasColumnName("MEMBER_ID");

        builder.Property(e => e.ProductId)
            .HasColumnName("PRODUCT_ID")
            .IsRequired();

        // Enum conversions
        builder.Property(e => e.InstallmentType)
            .HasColumnName("INSTALLMENT_TYPE")
            .HasConversion<int>()
            .IsRequired();

        builder.Property(e => e.Status)
            .HasColumnName("STATUS")
            .HasConversion<int>()
            .IsRequired();

        builder.Property(e => e.Type)
            .HasColumnName("TYPE")
            .HasConversion<int>()
            .IsRequired();

        // Decimal precision for monetary values
        builder.Property(e => e.Amount)
            .HasColumnName("AMOUNT")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(e => e.Tax)
            .HasColumnName("TAX")
            .HasPrecision(18, 2)
            .IsRequired();

        // Date
        builder.Property(e => e.DueDate)
            .HasColumnName("DUE_DATE")
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

        // Relationships
        builder.HasOne(e => e.BillingAccount)
            .WithMany(ba => ba.BillingInstallments)
            .HasForeignKey(e => e.BillingAccountId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_BILLING_INSTALLMENTS_ACCOUNT");

        // Indexes
        builder.HasIndex(e => e.BillingAccountId)
            .HasDatabaseName("IX_BILLING_INSTALLMENTS_ACCOUNT_ID");

        builder.HasIndex(e => e.DueDate)
            .HasDatabaseName("IX_BILLING_INSTALLMENTS_DUE_DATE");
    }
}
