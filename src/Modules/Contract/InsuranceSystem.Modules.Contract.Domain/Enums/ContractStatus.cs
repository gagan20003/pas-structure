// File: InsuranceSystem.Modules.Contract.Domain/Enums/ContractStatus.cs
namespace InsuranceSystem.Modules.Contract.Domain.Enums;

/// <summary>
/// Defines the status of a contract.
/// </summary>
public enum ContractStatus
{
    /// <summary>
    /// Contract is in draft state.
    /// </summary>
    Draft = 1,

    /// <summary>
    /// Contract is active.
    /// </summary>
    Active = 2,

    /// <summary>
    /// Contract is suspended.
    /// </summary>
    Suspended = 3,

    /// <summary>
    /// Contract has been terminated.
    /// </summary>
    Terminated = 4,

    /// <summary>
    /// Contract has expired.
    /// </summary>
    Expired = 5,

    /// <summary>
    /// Contract is pending renewal.
    /// </summary>
    PendingRenewal = 6
}
