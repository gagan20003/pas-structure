// File: InsuranceSystem.Modules.Contract.Domain/Enums/EndorsementStatus.cs
namespace InsuranceSystem.Modules.Contract.Domain.Enums;

/// <summary>
/// Defines the status of an endorsement.
/// </summary>
public enum EndorsementStatus
{
    /// <summary>
    /// Endorsement is pending approval.
    /// </summary>
    Pending = 1,

    /// <summary>
    /// Endorsement has been approved.
    /// </summary>
    Approved = 2,

    /// <summary>
    /// Endorsement has been rejected.
    /// </summary>
    Rejected = 3,

    /// <summary>
    /// Endorsement has been processed/applied.
    /// </summary>
    Processed = 4,

    /// <summary>
    /// Endorsement has been cancelled.
    /// </summary>
    Cancelled = 5
}
