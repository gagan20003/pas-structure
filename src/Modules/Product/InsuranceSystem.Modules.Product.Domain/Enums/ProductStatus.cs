// File: InsuranceSystem.Modules.Product.Domain/Enums/ProductStatus.cs
namespace InsuranceSystem.Modules.Product.Domain.Enums;

/// <summary>
/// Defines the status of an insurance product.
/// </summary>
public enum ProductStatus
{
    /// <summary>
    /// Product is active and available for sale.
    /// </summary>
    Active = 1,

    /// <summary>
    /// Product is inactive and not available for new sales.
    /// </summary>
    Inactive = 2,

    /// <summary>
    /// Product has been discontinued.
    /// </summary>
    Discontinued = 3,

    /// <summary>
    /// Product is in development/pending approval.
    /// </summary>
    Draft = 4
}
