// File: InsuranceSystem.Modules.Billing.Domain/Enums/PaymentStatus.cs
namespace InsuranceSystem.Modules.Billing.Domain.Enums;

/// <summary>
/// Defines the payment processing status.
/// </summary>
public enum PaymentStatus
{
    /// <summary>
    /// Payment is pending processing.
    /// </summary>
    Pending = 1,

    /// <summary>
    /// Payment has been completed successfully.
    /// </summary>
    Completed = 2,

    /// <summary>
    /// Payment has been cancelled.
    /// </summary>
    Cancelled = 3
}
