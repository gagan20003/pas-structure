// File: InsuranceSystem.Modules.Billing.Domain/Enums/PaymentMode.cs
namespace InsuranceSystem.Modules.Billing.Domain.Enums;

/// <summary>
/// Defines the payment mode/method.
/// </summary>
public enum PaymentMode
{
    /// <summary>
    /// Cash payment.
    /// </summary>
    Cash = 1,

    /// <summary>
    /// Card payment (credit/debit).
    /// </summary>
    Card = 2,

    /// <summary>
    /// Cheque payment.
    /// </summary>
    Cheque = 3
}
