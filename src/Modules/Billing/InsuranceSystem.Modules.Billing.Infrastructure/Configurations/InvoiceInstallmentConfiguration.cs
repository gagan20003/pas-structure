// File: InsuranceSystem.Modules.Billing.Infrastructure/Configurations/InvoiceInstallmentConfiguration.cs
using InsuranceSystem.Modules.Billing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsuranceSystem.Modules.Billing.Infrastructure.Configurations;

/// <summary>
/// EF Core configuration for InvoiceInstallment junction entity.
/// </summary>
public class InvoiceInstallmentConfiguration : IEntityTypeConfiguration<InvoiceInstallment>
{
    public void Configure(EntityTypeBuilder<InvoiceInstallment> builder)
    {
        builder.ToTable("INVOICE_INSTALLMENTS");

        // Primary Key with Oracle IDENTITY
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasColumnName("ID")
            .UseIdentityColumn();

        // Foreign Keys
        builder.Property(e => e.InvoiceId)
            .HasColumnName("INVOICE_ID")
            .IsRequired();

        builder.Property(e => e.BillingInstallmentId)
            .HasColumnName("BILLING_INSTALLMENT_ID")
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
        builder.HasOne(e => e.Invoice)
            .WithMany(i => i.InvoiceInstallments)
            .HasForeignKey(e => e.InvoiceId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_INVOICE_INSTALLMENTS_INVOICE");

        builder.HasOne(e => e.BillingInstallment)
            .WithMany(bi => bi.InvoiceInstallments)
            .HasForeignKey(e => e.BillingInstallmentId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_INVOICE_INSTALLMENTS_BILLING_INSTALLMENT");

        // Indexes
        builder.HasIndex(e => e.InvoiceId)
            .HasDatabaseName("IX_INVOICE_INSTALLMENTS_INVOICE_ID");

        builder.HasIndex(e => e.BillingInstallmentId)
            .HasDatabaseName("IX_INVOICE_INSTALLMENTS_BILLING_INSTALLMENT_ID");

        // Unique constraint to prevent duplicate mappings
        builder.HasIndex(e => new { e.InvoiceId, e.BillingInstallmentId })
            .IsUnique()
            .HasDatabaseName("IX_INVOICE_INSTALLMENTS_UNIQUE");
    }
}
