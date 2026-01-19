// File: InsuranceSystem.Modules.Product.Domain/Entities/Product.cs
using InsuranceSystem.Modules.Product.Domain.Enums;
using InsuranceSystem.SharedKernel.Domain;

namespace InsuranceSystem.Modules.Product.Domain.Entities;

/// <summary>
/// Represents an insurance product in the system.
/// </summary>
public class Product : AuditableEntity
{
    /// <summary>
    /// Unique product code identifier.
    /// </summary>
    public required string ProductCode { get; set; }

    /// <summary>
    /// Product name.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Detailed description of the product.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Type of insurance product.
    /// </summary>
    public required ProductType ProductType { get; set; }

    /// <summary>
    /// Current status of the product.
    /// </summary>
    public required ProductStatus Status { get; set; }

    /// <summary>
    /// Currency identifier for pricing.
    /// </summary>
    public required int CurrencyId { get; set; }

    /// <summary>
    /// Base premium amount.
    /// </summary>
    public required decimal BasePremium { get; set; }

    /// <summary>
    /// Date when the product becomes effective.
    /// </summary>
    public required DateOnly EffectiveDate { get; set; }

    /// <summary>
    /// Date when the product expires (if applicable).
    /// </summary>
    public DateOnly? ExpirationDate { get; set; }

    /// <summary>
    /// Maximum coverage amount.
    /// </summary>
    public decimal? MaxCoverageAmount { get; set; }

    /// <summary>
    /// Deductible amount.
    /// </summary>
    public decimal? DeductibleAmount { get; set; }

    // Domain Behaviors

    /// <summary>
    /// Activates the product for sale.
    /// </summary>
    public void Activate()
    {
        if (Status == ProductStatus.Discontinued)
            throw new InvalidOperationException("Cannot activate a discontinued product.");
            
        Status = ProductStatus.Active;
    }

    /// <summary>
    /// Deactivates the product (removes from sale but keeps existing policies valid).
    /// </summary>
    public void Deactivate()
    {
        Status = ProductStatus.Inactive;
    }

    /// <summary>
    /// Discontinues the product permanently.
    /// </summary>
    public void Discontinue()
    {
        Status = ProductStatus.Discontinued;
    }

    /// <summary>
    /// Checks if the product is available for new sales.
    /// </summary>
    /// <returns>True if available, false otherwise.</returns>
    public bool IsAvailableForSale()
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        return Status == ProductStatus.Active 
            && EffectiveDate <= today 
            && (ExpirationDate == null || ExpirationDate > today);
    }

    /// <summary>
    /// Calculates the premium with a given multiplier (e.g., for age or risk factors).
    /// </summary>
    /// <param name="multiplier">The premium multiplier.</param>
    /// <returns>Calculated premium amount.</returns>
    public decimal CalculatePremium(decimal multiplier)
    {
        if (multiplier <= 0)
            throw new ArgumentException("Multiplier must be positive.", nameof(multiplier));
            
        return BasePremium * multiplier;
    }
}
