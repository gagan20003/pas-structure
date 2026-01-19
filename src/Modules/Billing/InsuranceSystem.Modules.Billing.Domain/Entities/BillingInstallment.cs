// File: InsuranceSystem.Modules.Billing.Domain/Entities/BillingInstallment.cs
using InsuranceSystem.Modules.Billing.Domain.Enums;
using InsuranceSystem.SharedKernel.Domain;

namespace InsuranceSystem.Modules.Billing.Domain.Entities;

/// <summary>
/// Represents a billing installment record linked to a billing account.
/// </summary>
public class BillingInstallment : AuditableEntity
{
    /// <summary>
    /// Foreign key reference to the billing account.
    /// </summary>
    public required int BillingAccountId { get; set; }

    /// <summary>
    /// Foreign key reference to the endorsement (if applicable).
    /// </summary>
    public int? EndorsementId { get; set; }

    /// <summary>
    /// Type of installment.
    /// </summary>
    public required InstallmentType InstallmentType { get; set; }

    /// <summary>
    /// Foreign key reference to the contract.
    /// </summary>
    public required int ContractId { get; set; }

    /// <summary>
    /// Foreign key reference to the member (if applicable).
    /// </summary>
    public int? MemberId { get; set; }

    /// <summary>
    /// Foreign key reference to the product.
    /// </summary>
    public required int ProductId { get; set; }

    /// <summary>
    /// Installment amount.
    /// </summary>
    public required decimal Amount { get; set; }

    /// <summary>
    /// Due date for the installment.
    /// </summary>
    public required DateOnly DueDate { get; set; }

    /// <summary>
    /// Current status of the installment.
    /// </summary>
    public required Status Status { get; set; }

    /// <summary>
    /// Transaction type (Credit/Debit).
    /// </summary>
    public required TransactionType Type { get; set; }

    /// <summary>
    /// Tax amount for the installment.
    /// </summary>
    public required decimal Tax { get; set; }

    // Navigation Properties

    /// <summary>
    /// Reference to the parent billing account.
    /// </summary>
    public BillingAccount BillingAccount { get; set; } = null!;

    /// <summary>
    /// Collection of invoice installments linking this to invoices.
    /// </summary>
    public ICollection<InvoiceInstallment> InvoiceInstallments { get; set; } = [];

    // Domain Behaviors

    /// <summary>
    /// Calculates the total amount including tax.
    /// </summary>
    /// <returns>Total amount with tax.</returns>
    public decimal CalculateTotalWithTax()
    {
        return Amount + Tax;
    }

    /// <summary>
    /// Checks if the installment is overdue.
    /// </summary>
    /// <returns>True if overdue, false otherwise.</returns>
    public bool IsOverdue()
    {
        return Status == Status.Active && DueDate < DateOnly.FromDateTime(DateTime.UtcNow);
    }
}
