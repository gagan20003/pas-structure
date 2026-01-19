// File: InsuranceSystem.Modules.Billing.Infrastructure/Configurations/PaymentConfiguration.cs
using InsuranceSystem.Modules.Billing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsuranceSystem.Modules.Billing.Infrastructure.Configurations;

/// <summary>
/// EF Core configuration for Payment entity.
/// </summary>
public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("PAYMENTS");

        // Primary Key with Oracle IDENTITY
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasColumnName("ID")
            .UseIdentityColumn();

        // Foreign Keys
        builder.Property(e => e.BillingAccountId)
            .HasColumnName("BILLING_ACCOUNT_ID")
            .IsRequired();

        builder.Property(e => e.InvoiceId)
            .HasColumnName("INVOICE_ID");

        // Decimal precision for monetary values
        builder.Property(e => e.Amount)
            .HasColumnName("AMOUNT")
            .HasPrecision(18, 2)
            .IsRequired();

        // Date
        builder.Property(e => e.PaymentDate)
            .HasColumnName("PAYMENT_DATE")
            .IsRequired();

        // Enum conversions
        builder.Property(e => e.Mode)
            .HasColumnName("MODE")
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

        builder.Property(e => e.ReferenceNumber)
            .HasColumnName("REFERENCE_NUMBER")
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
            .WithMany(ba => ba.Payments)
            .HasForeignKey(e => e.BillingAccountId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_PAYMENTS_BILLING_ACCOUNT");

        builder.HasOne(e => e.Invoice)
            .WithMany(i => i.Payments)
            .HasForeignKey(e => e.InvoiceId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_PAYMENTS_INVOICE");

        // Indexes
        builder.HasIndex(e => e.BillingAccountId)
            .HasDatabaseName("IX_PAYMENTS_BILLING_ACCOUNT_ID");

        builder.HasIndex(e => e.InvoiceId)
            .HasDatabaseName("IX_PAYMENTS_INVOICE_ID");

        builder.HasIndex(e => e.ReferenceNumber)
            .HasDatabaseName("IX_PAYMENTS_REFERENCE_NUMBER");

        builder.HasIndex(e => e.PaymentDate)
            .HasDatabaseName("IX_PAYMENTS_PAYMENT_DATE");
    }
}
