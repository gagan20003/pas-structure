// File: InsuranceSystem.Modules.Billing.Domain/Enums/InstallmentType.cs
namespace InsuranceSystem.Modules.Billing.Domain.Enums;

/// <summary>
/// Defines the type of billing installment.
/// </summary>
public enum InstallmentType
{
    /// <summary>
    /// Premium installment.
    /// </summary>
    Premium = 1,

    /// <summary>
    /// Fee installment.
    /// </summary>
    Fee = 2,

    /// <summary>
    /// Adjustment installment.
    /// </summary>
    Adjustment = 3,

    /// <summary>
    /// Refund installment.
    /// </summary>
    Refund = 4
}
