// File: InsuranceSystem.Modules.Billing.Domain/Enums/InvoiceStatus.cs
namespace InsuranceSystem.Modules.Billing.Domain.Enums;

/// <summary>
/// Defines the invoice processing status.
/// </summary>
public enum InvoiceStatus
{
    /// <summary>
    /// Invoice is in draft state.
    /// </summary>
    Draft = 1,

    /// <summary>
    /// Invoice has been issued and is pending payment.
    /// </summary>
    Issued = 2,

    /// <summary>
    /// Invoice has been partially paid.
    /// </summary>
    PartiallyPaid = 3,

    /// <summary>
    /// Invoice has been fully paid.
    /// </summary>
    Paid = 4,

    /// <summary>
    /// Invoice has been cancelled.
    /// </summary>
    Cancelled = 5,

    /// <summary>
    /// Invoice is overdue.
    /// </summary>
    Overdue = 6
}
