// File: InsuranceSystem.Modules.Contract.Domain/Entities/MasterContract.cs
using InsuranceSystem.Modules.Contract.Domain.Enums;
using InsuranceSystem.SharedKernel.Domain;

namespace InsuranceSystem.Modules.Contract.Domain.Entities;

/// <summary>
/// Represents a master contract (group policy) in the system.
/// </summary>
public class MasterContract : AuditableEntity
{
    /// <summary>
    /// Unique master contract number.
    /// </summary>
    public required string MasterContractNumber { get; set; }

    /// <summary>
    /// Name of the policyholder/employer.
    /// </summary>
    public required string PolicyholderName { get; set; }

    /// <summary>
    /// Current status of the master contract.
    /// </summary>
    public required ContractStatus Status { get; set; }

    /// <summary>
    /// Currency identifier for the contract.
    /// </summary>
    public required int CurrencyId { get; set; }

    /// <summary>
    /// Date when the master contract becomes effective.
    /// </summary>
    public required DateOnly EffectiveDate { get; set; }

    /// <summary>
    /// Date when the master contract expires.
    /// </summary>
    public required DateOnly ExpirationDate { get; set; }

    /// <summary>
    /// Date when the master contract was terminated (if applicable).
    /// </summary>
    public DateOnly? TerminationDate { get; set; }

    // Navigation Properties

    /// <summary>
    /// Collection of contracts under this master contract.
    /// </summary>
    public ICollection<Contract> Contracts { get; set; } = [];

    // Domain Behaviors

    /// <summary>
    /// Activates the master contract.
    /// </summary>
    public void Activate()
    {
        if (Status == ContractStatus.Terminated || Status == ContractStatus.Expired)
            throw new InvalidOperationException("Cannot activate a terminated or expired contract.");
            
        Status = ContractStatus.Active;
    }

    /// <summary>
    /// Suspends the master contract.
    /// </summary>
    public void Suspend()
    {
        if (Status != ContractStatus.Active)
            throw new InvalidOperationException("Only active contracts can be suspended.");
            
        Status = ContractStatus.Suspended;
    }

    /// <summary>
    /// Terminates the master contract.
    /// </summary>
    /// <param name="terminationDate">The date of termination.</param>
    public void Terminate(DateOnly terminationDate)
    {
        Status = ContractStatus.Terminated;
        TerminationDate = terminationDate;
    }

    /// <summary>
    /// Checks if the master contract is currently active.
    /// </summary>
    /// <returns>True if active, false otherwise.</returns>
    public bool IsActive()
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        return Status == ContractStatus.Active 
            && EffectiveDate <= today 
            && ExpirationDate > today;
    }
}
