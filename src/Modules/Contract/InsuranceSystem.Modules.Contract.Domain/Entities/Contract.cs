// File: InsuranceSystem.Modules.Contract.Domain/Entities/Contract.cs
using InsuranceSystem.Modules.Contract.Domain.Enums;
using InsuranceSystem.SharedKernel.Domain;

namespace InsuranceSystem.Modules.Contract.Domain.Entities;

/// <summary>
/// Represents an individual contract/policy in the system.
/// </summary>
public class Contract : AuditableEntity
{
    /// <summary>
    /// Unique contract number.
    /// </summary>
    public required string ContractNumber { get; set; }

    /// <summary>
    /// Foreign key reference to the master contract.
    /// </summary>
    public required int MasterContractId { get; set; }

    /// <summary>
    /// Foreign key reference to the product.
    /// </summary>
    public required int ProductId { get; set; }

    /// <summary>
    /// Current status of the contract.
    /// </summary>
    public required ContractStatus Status { get; set; }

    /// <summary>
    /// Currency identifier for the contract.
    /// </summary>
    public required int CurrencyId { get; set; }

    /// <summary>
    /// Total premium amount for the contract.
    /// </summary>
    public required decimal PremiumAmount { get; set; }

    /// <summary>
    /// Date when the contract becomes effective.
    /// </summary>
    public required DateOnly EffectiveDate { get; set; }

    /// <summary>
    /// Date when the contract expires.
    /// </summary>
    public required DateOnly ExpirationDate { get; set; }

    /// <summary>
    /// Date when the contract was terminated (if applicable).
    /// </summary>
    public DateOnly? TerminationDate { get; set; }

    // Navigation Properties

    /// <summary>
    /// Reference to the parent master contract.
    /// </summary>
    public MasterContract MasterContract { get; set; } = null!;

    /// <summary>
    /// Collection of endorsements for this contract.
    /// </summary>
    public ICollection<Endorsement> Endorsements { get; set; } = [];

    // Domain Behaviors

    /// <summary>
    /// Activates the contract.
    /// </summary>
    public void Activate()
    {
        if (Status == ContractStatus.Terminated || Status == ContractStatus.Expired)
            throw new InvalidOperationException("Cannot activate a terminated or expired contract.");
            
        Status = ContractStatus.Active;
    }

    /// <summary>
    /// Terminates the contract.
    /// </summary>
    /// <param name="terminationDate">The date of termination.</param>
    public void Terminate(DateOnly terminationDate)
    {
        Status = ContractStatus.Terminated;
        TerminationDate = terminationDate;
    }

    /// <summary>
    /// Checks if the contract is currently active.
    /// </summary>
    /// <returns>True if active, false otherwise.</returns>
    public bool IsActive()
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        return Status == ContractStatus.Active 
            && EffectiveDate <= today 
            && ExpirationDate > today;
    }

    /// <summary>
    /// Gets the remaining coverage days.
    /// </summary>
    /// <returns>Number of days until expiration.</returns>
    public int GetRemainingDays()
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        if (ExpirationDate <= today)
            return 0;
            
        return ExpirationDate.DayNumber - today.DayNumber;
    }
}
