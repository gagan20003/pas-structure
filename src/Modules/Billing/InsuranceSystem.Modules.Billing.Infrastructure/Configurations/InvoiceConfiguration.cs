// File: InsuranceSystem.Modules.Billing.Infrastructure/Configurations/InvoiceConfiguration.cs
using InsuranceSystem.Modules.Billing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsuranceSystem.Modules.Billing.Infrastructure.Configurations;

/// <summary>
/// EF Core configuration for Invoice entity.
/// </summary>
public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("INVOICES");

        // Primary Key with Oracle IDENTITY
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasColumnName("ID")
            .UseIdentityColumn();

        // Foreign Key
        builder.Property(e => e.BillingAccountId)
            .HasColumnName("BILLING_ACCOUNT_ID")
            .IsRequired();

        // Properties
        builder.Property(e => e.InvoiceNumber)
            .HasColumnName("INVOICE_NUMBER")
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

        // Enum conversion
        builder.Property(e => e.Status)
            .HasColumnName("STATUS")
            .HasConversion<int>()
            .IsRequired();

        // Cancellation fields
        builder.Property(e => e.CancelledOn)
            .HasColumnName("CANCELLED_ON");

        builder.Property(e => e.CancelledReason)
            .HasColumnName("CANCELLED_REASON")
            .HasMaxLength(500);

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
            .WithMany(ba => ba.Invoices)
            .HasForeignKey(e => e.BillingAccountId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_INVOICES_BILLING_ACCOUNT");

        // Indexes
        builder.HasIndex(e => e.BillingAccountId)
            .HasDatabaseName("IX_INVOICES_BILLING_ACCOUNT_ID");

        builder.HasIndex(e => e.InvoiceNumber)
            .IsUnique()
            .HasDatabaseName("IX_INVOICES_INVOICE_NUMBER");

        builder.HasIndex(e => e.Status)
            .HasDatabaseName("IX_INVOICES_STATUS");
    }
}
