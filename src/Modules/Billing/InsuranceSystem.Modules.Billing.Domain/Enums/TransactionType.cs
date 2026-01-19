// File: InsuranceSystem.Modules.Billing.Domain/Enums/TransactionType.cs
namespace InsuranceSystem.Modules.Billing.Domain.Enums;

/// <summary>
/// Defines the transaction type for billing operations.
/// </summary>
public enum TransactionType
{
    /// <summary>
    /// Credit transaction (money received/added).
    /// </summary>
    Credit = 1,

    /// <summary>
    /// Debit transaction (money paid/deducted).
    /// </summary>
    Debit = 2
}
