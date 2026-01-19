// File: InsuranceSystem.Modules.Billing.Domain/Entities/Invoice.cs
using InsuranceSystem.Modules.Billing.Domain.Enums;
using InsuranceSystem.SharedKernel.Domain;

namespace InsuranceSystem.Modules.Billing.Domain.Entities;

/// <summary>
/// Represents an invoice for billing purposes.
/// </summary>
public class Invoice : AuditableEntity
{
    /// <summary>
    /// Foreign key reference to the billing account.
    /// </summary>
    public required int BillingAccountId { get; set; }

    /// <summary>
    /// Unique invoice number identifier.
    /// </summary>
    public required int InvoiceNumber { get; set; }

    /// <summary>
    /// Total invoice amount (sum of line items).
    /// </summary>
    public required decimal Amount { get; set; }

    /// <summary>
    /// Total tax amount (sum of line item taxes).
    /// </summary>
    public required decimal Tax { get; set; }

    /// <summary>
    /// Current status of the invoice.
    /// </summary>
    public required InvoiceStatus Status { get; set; }

    /// <summary>
    /// Date when the invoice was cancelled (if applicable).
    /// </summary>
    public DateTime? CancelledOn { get; set; }

    /// <summary>
    /// Reason for cancellation (if applicable).
    /// </summary>
    public string? CancelledReason { get; set; }

    // Navigation Properties

    /// <summary>
    /// Reference to the parent billing account.
    /// </summary>
    public BillingAccount BillingAccount { get; set; } = null!;

    /// <summary>
    /// Collection of payments associated with this invoice.
    /// </summary>
    public ICollection<Payment> Payments { get; set; } = [];

    /// <summary>
    /// Collection of invoice installments linking this invoice to billing installments.
    /// </summary>
    public ICollection<InvoiceInstallment> InvoiceInstallments { get; set; } = [];

    // Domain Behaviors

    /// <summary>
    /// Calculates the total invoice amount including tax.
    /// </summary>
    /// <returns>Total amount with tax.</returns>
    public decimal CalculateTotalWithTax()
    {
        return Amount + Tax;
    }

    /// <summary>
    /// Calculates the total amount paid on this invoice.
    /// </summary>
    /// <returns>Total paid amount.</returns>
    public decimal CalculateTotalPaid()
    {
        return Payments
            .Where(p => p.Status == PaymentStatus.Completed)
            .Sum(p => p.Amount);
    }

    /// <summary>
    /// Calculates the remaining balance on this invoice.
    /// </summary>
    /// <returns>Remaining balance.</returns>
    public decimal CalculateBalance()
    {
        return CalculateTotalWithTax() - CalculateTotalPaid();
    }

    /// <summary>
    /// Cancels the invoice with a reason.
    /// </summary>
    /// <param name="reason">The cancellation reason.</param>
    public void Cancel(string reason)
    {
        if (Status == InvoiceStatus.Cancelled)
            throw new InvalidOperationException("Invoice is already cancelled.");

        Status = InvoiceStatus.Cancelled;
        CancelledOn = DateTime.UtcNow;
        CancelledReason = reason;
    }

    /// <summary>
    /// Issues the invoice (moves from Draft to Issued).
    /// </summary>
    public void Issue()
    {
        if (Status != InvoiceStatus.Draft)
            throw new InvalidOperationException("Only draft invoices can be issued.");

        Status = InvoiceStatus.Issued;
    }

    /// <summary>
    /// Updates the payment status based on payments received.
    /// </summary>
    public void UpdatePaymentStatus()
    {
        var totalPaid = CalculateTotalPaid();
        var totalDue = CalculateTotalWithTax();

        if (totalPaid >= totalDue)
            Status = InvoiceStatus.Paid;
        else if (totalPaid > 0)
            Status = InvoiceStatus.PartiallyPaid;
    }
}
