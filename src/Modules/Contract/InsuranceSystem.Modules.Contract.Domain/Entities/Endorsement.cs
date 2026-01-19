// File: InsuranceSystem.Modules.Contract.Domain/Entities/Endorsement.cs
using InsuranceSystem.Modules.Contract.Domain.Enums;
using InsuranceSystem.SharedKernel.Domain;

namespace InsuranceSystem.Modules.Contract.Domain.Entities;

/// <summary>
/// Represents an endorsement (amendment) to a contract.
/// </summary>
public class Endorsement : AuditableEntity
{
    /// <summary>
    /// Unique endorsement number.
    /// </summary>
    public required string EndorsementNumber { get; set; }

    /// <summary>
    /// Foreign key reference to the contract.
    /// </summary>
    public required int ContractId { get; set; }

    /// <summary>
    /// Type of endorsement.
    /// </summary>
    public required EndorsementType EndorsementType { get; set; }

    /// <summary>
    /// Current status of the endorsement.
    /// </summary>
    public required EndorsementStatus Status { get; set; }

    /// <summary>
    /// Date when the endorsement becomes effective.
    /// </summary>
    public required DateOnly EffectiveDate { get; set; }

    /// <summary>
    /// Premium adjustment amount (can be positive or negative).
    /// </summary>
    public required decimal PremiumAdjustment { get; set; }

    /// <summary>
    /// Description or reason for the endorsement.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Date when the endorsement was processed (if applicable).
    /// </summary>
    public DateTime? ProcessedOn { get; set; }

    /// <summary>
    /// User who processed the endorsement (if applicable).
    /// </summary>
    public string? ProcessedBy { get; set; }

    // Navigation Properties

    /// <summary>
    /// Reference to the parent contract.
    /// </summary>
    public Contract Contract { get; set; } = null!;

    // Domain Behaviors

    /// <summary>
    /// Approves the endorsement.
    /// </summary>
    public void Approve()
    {
        if (Status != EndorsementStatus.Pending)
            throw new InvalidOperationException("Only pending endorsements can be approved.");
            
        Status = EndorsementStatus.Approved;
    }

    /// <summary>
    /// Rejects the endorsement.
    /// </summary>
    public void Reject()
    {
        if (Status != EndorsementStatus.Pending)
            throw new InvalidOperationException("Only pending endorsements can be rejected.");
            
        Status = EndorsementStatus.Rejected;
    }

    /// <summary>
    /// Processes the endorsement (applies the changes).
    /// </summary>
    /// <param name="processedBy">The user processing the endorsement.</param>
    public void Process(string processedBy)
    {
        if (Status != EndorsementStatus.Approved)
            throw new InvalidOperationException("Only approved endorsements can be processed.");
            
        Status = EndorsementStatus.Processed;
        ProcessedOn = DateTime.UtcNow;
        ProcessedBy = processedBy;
    }

    /// <summary>
    /// Cancels the endorsement.
    /// </summary>
    public void Cancel()
    {
        if (Status == EndorsementStatus.Processed)
            throw new InvalidOperationException("Cannot cancel a processed endorsement.");
            
        Status = EndorsementStatus.Cancelled;
    }
}
