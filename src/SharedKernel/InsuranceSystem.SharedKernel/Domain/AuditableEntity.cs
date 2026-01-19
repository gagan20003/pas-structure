// File: InsuranceSystem.SharedKernel/Domain/AuditableEntity.cs
namespace InsuranceSystem.SharedKernel.Domain;

/// <summary>
/// Base entity class for all auditable entities in the system.
/// Provides common audit fields for tracking creation and modification.
/// </summary>
public abstract class AuditableEntity
{
    /// <summary>
    /// Primary key identifier.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Timestamp when the entity was created.
    /// </summary>
    public required DateTime CreatedAt { get; set; }

    /// <summary>
    /// User or system identifier that created the entity.
    /// </summary>
    public required string CreatedBy { get; set; }

    /// <summary>
    /// Timestamp when the entity was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// User or system identifier that last updated the entity.
    /// </summary>
    public string? UpdatedBy { get; set; }

    /// <summary>
    /// Sets the creation audit fields.
    /// </summary>
    /// <param name="createdBy">The user or system creating the entity.</param>
    public void SetCreationAudit(string createdBy)
    {
        CreatedAt = DateTime.UtcNow;
        CreatedBy = createdBy;
    }

    /// <summary>
    /// Sets the modification audit fields.
    /// </summary>
    /// <param name="modifiedBy">The user or system modifying the entity.</param>
    public void SetModificationAudit(string modifiedBy)
    {
        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = modifiedBy;
    }
}
