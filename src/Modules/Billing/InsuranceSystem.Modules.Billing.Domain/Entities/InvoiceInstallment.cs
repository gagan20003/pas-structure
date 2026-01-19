// File: InsuranceSystem.Modules.Billing.Domain/Entities/InvoiceInstallment.cs
using InsuranceSystem.SharedKernel.Domain;

namespace InsuranceSystem.Modules.Billing.Domain.Entities;

/// <summary>
/// Junction entity linking invoices to billing installments.
/// </summary>
public class InvoiceInstallment : AuditableEntity
{
    /// <summary>
    /// Foreign key reference to the invoice.
    /// </summary>
    public required int InvoiceId { get; set; }

    /// <summary>
    /// Foreign key reference to the billing installment.
    /// </summary>
    public required int BillingInstallmentId { get; set; }

    // Navigation Properties

    /// <summary>
    /// Reference to the parent invoice.
    /// </summary>
    public Invoice Invoice { get; set; } = null!;

    /// <summary>
    /// Reference to the linked billing installment.
    /// </summary>
    public BillingInstallment BillingInstallment { get; set; } = null!;
}
