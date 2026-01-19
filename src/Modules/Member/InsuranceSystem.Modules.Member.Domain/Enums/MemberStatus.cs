// File: InsuranceSystem.Modules.Member.Domain/Enums/MemberStatus.cs
namespace InsuranceSystem.Modules.Member.Domain.Enums;

/// <summary>
/// Defines the status of a member.
/// </summary>
public enum MemberStatus
{
    /// <summary>
    /// Member is active.
    /// </summary>
    Active = 1,

    /// <summary>
    /// Member is inactive.
    /// </summary>
    Inactive = 2,

    /// <summary>
    /// Member is suspended.
    /// </summary>
    Suspended = 3,

    /// <summary>
    /// Member is pending activation.
    /// </summary>
    Pending = 4
}
