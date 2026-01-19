// File: InsuranceSystem.Modules.Contract.Domain/Enums/EndorsementType.cs
namespace InsuranceSystem.Modules.Contract.Domain.Enums;

/// <summary>
/// Defines the type of endorsement.
/// </summary>
public enum EndorsementType
{
    /// <summary>
    /// Addition of coverage or member.
    /// </summary>
    Addition = 1,

    /// <summary>
    /// Deletion of coverage or member.
    /// </summary>
    Deletion = 2,

    /// <summary>
    /// Modification of existing coverage.
    /// </summary>
    Modification = 3,

    /// <summary>
    /// Renewal of coverage.
    /// </summary>
    Renewal = 4,

    /// <summary>
    /// Cancellation of coverage.
    /// </summary>
    Cancellation = 5
}
