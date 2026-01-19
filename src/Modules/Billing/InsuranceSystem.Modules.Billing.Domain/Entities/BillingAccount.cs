// File: InsuranceSystem.Modules.Billing.Domain/Entities/BillingAccount.cs
using InsuranceSystem.Modules.Billing.Domain.Enums;
using InsuranceSystem.SharedKernel.Domain;

namespace InsuranceSystem.Modules.Billing.Domain.Entities;

/// <summary>
/// Represents a billing account for managing insurance-related financial transactions.
/// </summary>
public class BillingAccount : AuditableEntity
{
    /// <summary>
    /// Foreign key reference to the master contract.
    /// </summary>
    public required int MasterContractId { get; set; }

    /// <summary>
    /// Foreign key reference to the specific contract.
    /// </summary>
    public required int ContractId { get; set; }

    /// <summary>
    /// Currency identifier for monetary values.
    /// </summary>
    public required int CurrencyId { get; set; }

    /// <summary>
    /// Unique account number identifier (e.g., "ABC-001").
    /// </summary>
    public required string AccountNumber { get; set; }

    /// <summary>
    /// Type of billing account (Employer/Individual).
    /// </summary>
    public required BillingAccountType BillingAccountType { get; set; }

    /// <summary>
    /// Billing cycle frequency (Annual/Quarterly/Monthly).
    /// </summary>
    public required BillingCycle BillingCycle { get; set; }

    /// <summary>
    /// Current outstanding amount on the account.
    /// </summary>
    public required decimal OutstandingAmount { get; set; }

    /// <summary>
    /// Total amount billed to this account.
    /// </summary>
    public required decimal TotalBilledAmount { get; set; }

    /// <summary>
    /// Current status of the billing account.
    /// </summary>
    public required Status Status { get; set; }

    // Navigation Properties

    /// <summary>
    /// Collection of billing installments associated with this account.
    /// </summary>
    public ICollection<BillingInstallment> BillingInstallments { get; set; } = [];

    /// <summary>
    /// Collection of invoices associated with this account.
    /// </summary>
    public ICollection<Invoice> Invoices { get; set; } = [];

    /// <summary>
    /// Collection of payments received for this account.
    /// </summary>
    public ICollection<Payment> Payments { get; set; } = [];

    // Domain Behaviors

    /// <summary>
    /// Activates the billing account.
    /// </summary>
    public void Activate()
    {
        Status = Status.Active;
    }

    /// <summary>
    /// Deactivates the billing account.
    /// </summary>
    public void Deactivate()
    {
        Status = Status.Inactive;
    }

    /// <summary>
    /// Records a payment and updates the outstanding amount.
    /// </summary>
    /// <param name="amount">The payment amount to apply.</param>
    public void ApplyPayment(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Payment amount must be positive.", nameof(amount));

        OutstandingAmount -= amount;
    }

    /// <summary>
    /// Adds a charge to the account and updates outstanding and total billed amounts.
    /// </summary>
    /// <param name="amount">The charge amount to add.</param>
    public void AddCharge(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Charge amount must be positive.", nameof(amount));

        OutstandingAmount += amount;
        TotalBilledAmount += amount;
    }
}
