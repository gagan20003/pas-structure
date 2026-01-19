// File: InsuranceSystem.Modules.Member.Domain/Entities/Member.cs
using InsuranceSystem.Modules.Member.Domain.Enums;
using InsuranceSystem.SharedKernel.Domain;

namespace InsuranceSystem.Modules.Member.Domain.Entities;

/// <summary>
/// Represents an insured member in the system.
/// </summary>
public class Member : AuditableEntity
{
    /// <summary>
    /// Unique member number identifier.
    /// </summary>
    public required string MemberNumber { get; set; }

    /// <summary>
    /// Member's first name.
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Member's last name.
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Member's date of birth.
    /// </summary>
    public required DateOnly DateOfBirth { get; set; }

    /// <summary>
    /// Member's gender.
    /// </summary>
    public required Gender Gender { get; set; }

    /// <summary>
    /// Member's email address.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Member's phone number.
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Member's address.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Foreign key reference to the contract.
    /// </summary>
    public required int ContractId { get; set; }

    /// <summary>
    /// Current status of the member.
    /// </summary>
    public required MemberStatus Status { get; set; }

    /// <summary>
    /// Date when the member's coverage starts.
    /// </summary>
    public required DateOnly EffectiveDate { get; set; }

    /// <summary>
    /// Date when the member's coverage ends (if applicable).
    /// </summary>
    public DateOnly? TerminationDate { get; set; }

    // Domain Behaviors

    /// <summary>
    /// Gets the full name of the member.
    /// </summary>
    public string FullName => $"{FirstName} {LastName}";

    /// <summary>
    /// Calculates the member's current age.
    /// </summary>
    /// <returns>Age in years.</returns>
    public int CalculateAge()
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var age = today.Year - DateOfBirth.Year;
        
        if (DateOfBirth > today.AddYears(-age))
            age--;
            
        return age;
    }

    /// <summary>
    /// Activates the member.
    /// </summary>
    public void Activate()
    {
        Status = MemberStatus.Active;
    }

    /// <summary>
    /// Suspends the member.
    /// </summary>
    public void Suspend()
    {
        Status = MemberStatus.Suspended;
    }

    /// <summary>
    /// Terminates the member's coverage.
    /// </summary>
    /// <param name="terminationDate">The date of termination.</param>
    public void Terminate(DateOnly terminationDate)
    {
        Status = MemberStatus.Inactive;
        TerminationDate = terminationDate;
    }

    /// <summary>
    /// Checks if the member's coverage is currently active.
    /// </summary>
    /// <returns>True if active, false otherwise.</returns>
    public bool IsCoverageActive()
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        return Status == MemberStatus.Active 
            && EffectiveDate <= today 
            && (TerminationDate == null || TerminationDate > today);
    }
}
