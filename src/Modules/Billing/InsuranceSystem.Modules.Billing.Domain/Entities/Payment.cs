// File: InsuranceSystem.Modules.Billing.Domain/Entities/Payment.cs
using InsuranceSystem.Modules.Billing.Domain.Enums;
using InsuranceSystem.SharedKernel.Domain;

namespace InsuranceSystem.Modules.Billing.Domain.Entities;

/// <summary>
/// Represents a payment transaction.
/// </summary>
public class Payment : AuditableEntity
{
    /// <summary>
    /// Foreign key reference to the billing account.
    /// </summary>
    public required int BillingAccountId { get; set; }

    /// <summary>
    /// Foreign key reference to the invoice (if applicable).
    /// </summary>
    public int? InvoiceId { get; set; }

    /// <summary>
    /// Payment amount.
    /// </summary>
    public required decimal Amount { get; set; }

    /// <summary>
    /// Date when the payment was made.
    /// </summary>
    public required DateOnly PaymentDate { get; set; }

    /// <summary>
    /// Payment mode (Cash/Card/Cheque).
    /// </summary>
    public required PaymentMode Mode { get; set; }

    /// <summary>
    /// Payment reference number for tracking.
    /// </summary>
    public required int ReferenceNumber { get; set; }

    /// <summary>
    /// Current status of the payment.
    /// </summary>
    public required PaymentStatus Status { get; set; }

    /// <summary>
    /// Transaction type (Credit/Debit).
    /// </summary>
    public required TransactionType Type { get; set; }

    // Navigation Properties

    /// <summary>
    /// Reference to the parent billing account.
    /// </summary>
    public BillingAccount BillingAccount { get; set; } = null!;

    /// <summary>
    /// Reference to the associated invoice (if applicable).
    /// </summary>
    public Invoice? Invoice { get; set; }

    // Domain Behaviors

    /// <summary>
    /// Marks the payment as completed.
    /// </summary>
    public void Complete()
    {
        if (Status == PaymentStatus.Cancelled)
            throw new InvalidOperationException("Cannot complete a cancelled payment.");

        Status = PaymentStatus.Completed;
    }

    /// <summary>
    /// Cancels the payment.
    /// </summary>
    public void Cancel()
    {
        if (Status == PaymentStatus.Completed)
            throw new InvalidOperationException("Cannot cancel a completed payment.");

        Status = PaymentStatus.Cancelled;
    }

    /// <summary>
    /// Checks if the payment is pending.
    /// </summary>
    /// <returns>True if pending, false otherwise.</returns>
    public bool IsPending()
    {
        return Status == PaymentStatus.Pending;
    }
}
